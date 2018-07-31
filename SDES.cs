using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDESEncryption
{
    public class SDES
    {
        private bool _debug = false;

        // Key Variables
        private string _plaintextKey;
        private string _inputKey;
        private string _p10Key;
        private string _p10LeftHalf;
        private string _p10RightHalf;
        private string _keyOne;
        private string _keyTwo;

        // Encryption/Decryption Variables
        private string _mainInput;
        private string _mainOutput;
        private string _IPOutput;
        private string _IPLeft;
        private string _IPRight;
        private string _EP;
        private string _XEP;
        private string _XEPLeft;
        private string _XEPRight;
        private string _GridBitsLeft;
        private string _GridBitsRight;
        private string _CombinedGridBits;
        private string _P4;
        private string _RoundOneLeft;
        private string _RoundTwoLeft;
        private bool _encryption = true;

        private string[,] S0 = new string[,]
        {
            { "01", "00", "11", "10" },
            { "11", "10", "01", "00" },
            { "00", "10", "01", "11" },
            { "11", "01", "11", "10" }
        };

        private string[,] S1 = new string[,]
{
            { "00", "01", "10", "11" },
            { "10", "00", "01", "11" },
            { "11", "00", "01", "00" },
            { "10", "01", "00", "11" }
};

        public SDES()
        {
            Reset();
        }

        public void Reset()
        {
            _plaintextKey = _inputKey = _p10Key = _p10LeftHalf = _p10RightHalf = _keyOne = _keyTwo
                          = _mainInput = _mainOutput = _IPOutput = _IPLeft = _IPRight = _EP = _XEP = _XEPLeft
                          = _XEPRight = _GridBitsLeft = _GridBitsRight = _CombinedGridBits = _P4 = _RoundOneLeft = "";
        }

        public void DES(bool encryption)
        {
            // Round 1
            _encryption = encryption;
            Console.Write("Enter a 8-bit binary input: ");
            _mainInput = Console.ReadLine();
            while (_inputKey.Length != 8 && CheckBinary(_mainInput))
            {
                Console.WriteLine();
                Console.Write("That input was invalid. Please enter a 8-bit binary input: ");
                _mainInput = Console.ReadLine();
            }
            IPTransposition(_mainInput);
            SplitIPHalves();
            EPExpansion(_IPRight);
            _XEP = (_encryption) ? XOR(_EP, _keyOne) : XOR(_EP, _keyTwo);
            SplitEPHalves();
            CreateGridBits();
            P4Transposition();
            _RoundOneLeft = XOR(_P4, _IPLeft);

            // Round 2
            EPExpansion(_RoundOneLeft);
            _XEP = (_encryption) ? XOR(_EP, _keyTwo) : XOR(_EP, _keyOne);
            SplitEPHalves();
            CreateGridBits();
            P4Transposition();
            _RoundTwoLeft = XOR(_P4, _IPRight);
            InverseIPTransposition(_RoundTwoLeft, _RoundOneLeft);
        }

        public void IPTransposition(string input)
        {
            _IPOutput = "";
            _IPOutput = input[1].ToString() + input[5].ToString() + input[2].ToString() + input[0].ToString()
                       + input[3].ToString() + input[7].ToString() + input[4].ToString() + input[6].ToString();
        }

        public void InverseIPTransposition(string left, string right)
        {
            _mainOutput = "";
            _mainOutput = left[3].ToString() + left[0].ToString() + left[2].ToString() + right[0].ToString()
                        + right[2].ToString() + left[1].ToString() + right[3].ToString() + right[1].ToString();

            Console.WriteLine("Final Output is: {0}", _mainOutput);
        }

        public void SplitIPHalves()
        {
            _IPLeft = "";
            _IPRight = "";
            for (int i = 0; i < 4; i++)
            {
                _IPLeft += _IPOutput[i].ToString();
                _IPRight += _IPOutput[i + 4].ToString();
            }
        }

        public void SplitEPHalves()
        {
            _XEPLeft = "";
            _XEPRight = "";

            for (int i = 0; i < 4; i++)
            {
                _XEPLeft += _XEP[i].ToString();
                _XEPRight += _XEP[i + 4].ToString();
            }
        }

        public void EPExpansion(string input)
        {
            _EP = input[3].ToString() + input[0].ToString() + input[1].ToString() + input[2].ToString()
                 + input[1].ToString() + input[2].ToString() + input[3].ToString() + input[0].ToString();

            if (_debug)
                Console.WriteLine("EP are: {0}", _EP);
        }

        public void CreateGridBits()
        {
            string xValue = "";
            xValue = _XEPLeft[1].ToString() + _XEPLeft[2].ToString();
            int x = FindIntValue(xValue);

            string yValue = "";
            yValue = _XEPLeft[0].ToString() + _XEPLeft[3].ToString();
            int y = FindIntValue(yValue);

            _GridBitsLeft = S0[y, x];

            xValue = "";
            xValue = _XEPRight[1].ToString() + _XEPRight[2].ToString();
            x = FindIntValue(xValue);

            yValue = "";
            yValue = _XEPRight[0].ToString() + _XEPRight[3].ToString();
            y = FindIntValue(yValue);

            _GridBitsRight = S1[y, x];

            _CombinedGridBits = _GridBitsLeft + _GridBitsRight;

            if (_debug)
                Console.WriteLine("Gridbits are: {0}", _CombinedGridBits);
        }

        public void P4Transposition()
        {
            _P4 = _CombinedGridBits[1].ToString() + _CombinedGridBits[3].ToString()
                + _CombinedGridBits[2].ToString() + _CombinedGridBits[0].ToString();
            if (_debug)
                Console.WriteLine("P4 is: {0}", _P4);
        }

        public int FindIntValue(string binary)
        {
            int value = 0;
            switch (binary)
            {
                case "00":
                    value = 0;
                    break;
                case "01":
                    value = 1;
                    break;
                case "10":
                    value = 2;
                    break;
                case "11":
                    value = 3;
                    break;
            }

            return value;
        }

        public string XOR(string left, string right)
        {
            string finalValue = "";

            for (int i = 0; i < left.Length; i++)
            {
                if ((left[i].Equals('0') && right[i].Equals('0')) || (left[i].Equals('1') && right[i].Equals('1')))
                {
                    finalValue += "0";
                }
                else
                {
                    finalValue += "1";
                }
            }

            if (_debug)
                Console.WriteLine("XOR Output is: {0}", finalValue);

            return finalValue;
        }

        #region KeyCreation
        public void CreateKeys()
        {
            Console.Write("Enter a 10-bit binary key: ");
            _inputKey = Console.ReadLine();
            while (_inputKey.Length != 10 && CheckBinary(_inputKey))
            {
                Console.WriteLine();
                Console.Write("That input was invalid. Please enter a 10-bit binary key: ");
                _inputKey = Console.ReadLine();
            }

            _p10Key = P10Transposition(_inputKey);
            SplitP10();

            _keyOne = P8Transposition(CombineHalves(_p10LeftHalf, _p10RightHalf, 1));
            Console.WriteLine("Key 1 is: {0}", _keyOne);

            _keyTwo = P8Transposition(CombineHalves(_p10LeftHalf, _p10RightHalf, 3));
            Console.WriteLine("Key 2 is: {0}", _keyTwo);
        }

        public string P10Transposition(string input)
        {
            string p10 = "";

            p10 = input[2].ToString() + input[4].ToString() + input[1].ToString()
                + input[6].ToString() + input[3].ToString() + input[9].ToString()
                + input[0].ToString() + input[8].ToString() + input[7].ToString() + input[5].ToString();

            if (_debug)
            {
                Console.WriteLine("P10 is: {0}", p10);
                Console.ReadKey();
            }
            return p10;
        }

        public string P8Transposition(string input)
        {
            string p8Key = ""; 
            p8Key = input[5].ToString() + input[2].ToString() + input[6].ToString()
                    + input[3].ToString() + input[7].ToString() + input[4].ToString()
                    + input[9].ToString() + input[8].ToString();

            return p8Key;
        }

        public void SplitP10()
        {
            _p10LeftHalf = "";
            _p10RightHalf = "";
            for (int i = 0; i < 5; i++)
            {
                _p10LeftHalf += _p10Key[i].ToString();
                _p10RightHalf += _p10Key[i + 5].ToString();
            }

            if (_debug)
            {
                Console.WriteLine("P10 Left is: {0}", _p10LeftHalf);
                Console.WriteLine("P10 Right is: {0}", _p10RightHalf);
                Console.ReadKey();
            }
        }

        public string CombineHalves(string leftHalf, string rightHalf, int shiftAmount)
        {
            leftHalf = leftHalf.Substring(shiftAmount, leftHalf.Length - shiftAmount) + leftHalf.Substring(0, shiftAmount);
            rightHalf = rightHalf.Substring(shiftAmount, rightHalf.Length - shiftAmount) + rightHalf.Substring(0, shiftAmount);

            string whole = leftHalf + rightHalf;

            return whole;
        }

        private bool CheckBinary(string value)
        {
            bool allBinary = true;
            foreach(char c in value)
            {
                if (c != '1' || c != '0')
                {
                    allBinary = false;
                }
            }
            return allBinary;
        }
        #endregion
    }
}
