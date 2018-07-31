# Encryption-Tool
Caesar encryption/decryption, Caesar cracking, Simplified DES encryption/decryption

This menu driven tool contains a couple methods of encryption/decryption.

The caesar encryptor will take in a plaintext or cipher text file and encrypt or decrypt the message
accordingly based upon the user defined key and output a text file containing the message.

The Caesar crack will take in a user file and decipher the message cycling through keys and comparing
results based on commonly used english words. After finding a match it will output only the correctly
deciphered message.

The SDES is a simplified DES encrypor/decryptor for binary digits. It will take in a 10 bit user key
that will be manipulated and eventually reduced to two 8 bit keys that will be used in the encryption 
of an 8 bit plaintext/ciphertext. Through the process the bits are manipulated through splits,
rotations, expansions/reductions, and put through an XY grid to have the data shuffled around even 
farther. Interesting things to note about that process is that the encryption and decryption work on 
the same process with the only difference being which key is applied first.
