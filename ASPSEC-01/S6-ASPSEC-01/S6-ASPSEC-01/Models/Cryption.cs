namespace S6_ASPSEC_01.Models
{
    public class EncryptionFunctions
    {
        public string ceasar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
        char[] CeasarArray; 

        public class Crypt
        {
            public string CryptionInput { get; set; }
            public string CryptedOutput { get; set; }
        }

        public string CryptedOutput { get; private set; }

        public EncryptionFunctions()
        {
            CeasarArray = ceasar.ToCharArray();
        }

        public string Encrypt(string CryptionInput)
        {
            char[] encryptedArray = new char[CryptionInput.Length];

            for (int i = 0; i < CryptionInput.Length; i++)
            {
                char currentChar = CryptionInput[i];
                int index = Array.IndexOf(CeasarArray, currentChar);

                if (index == -1)
                {
                    encryptedArray[i] = currentChar;
                }
                else
                {
                    int newIndex = (index + 5) % CeasarArray.Length;
                    encryptedArray[i] = CeasarArray[newIndex];
                }
            }

            CryptedOutput = new string(encryptedArray);
            return CryptedOutput;
        }

        public string Decrypt(string CryptionInput)
        {
            char[] encryptedArray = new char[CryptionInput.Length];

            for (int i = 0; i < CryptionInput.Length; i++)
            {
                char currentChar = CryptionInput[i];
                int index = Array.IndexOf(CeasarArray, currentChar);

                if (index == -1)
                {
                    encryptedArray[i] = currentChar;
                }
                else
                {
                    int newIndex = (index - 5) % CeasarArray.Length;
                    encryptedArray[i] = CeasarArray[newIndex];
                }
            }

            CryptedOutput = new string(encryptedArray);
            return CryptedOutput;
        }
    }
}
