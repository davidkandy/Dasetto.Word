using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Dasetto.Word
{
    public static class SecurityStringHelpers
    {
        /// <summary>
        /// Unsecures a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string Unsecure (this SecureString secureString)
        {
            // Make sure you have a secure string
            if (secureString == null)
            return string.Empty;

            // Get a pointer for an unsecure string in memory 
            var unmanangedString = IntPtr.Zero; 

            try
            {
                // Unsecures the password
                unmanangedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanangedString);
            }
            finally
            {
                //Cleans up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanangedString);
            }
        }
    }
}
