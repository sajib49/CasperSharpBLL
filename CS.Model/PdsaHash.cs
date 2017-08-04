using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CS.Model
{
    public class PdsaHash
        {
            private PdsaHashType _mbytHashType;
            private string _mstrOriginalString;
            private string _mstrHashString;
            private HashAlgorithm _mhash;
            bool _mboolUseSalt;
            string _mstrSaltValue = String.Empty;
            short _msrtSaltLength = 8;

            public enum PdsaHashType : byte
            {
                MD5,
                SHA1,
                SHA256,
                SHA384,
                SHA512,
                SHA513
            }

            #region "Public Properties"
            public PdsaHashType HashType
            {
                get { return _mbytHashType; }
                set
                {
                    if (_mbytHashType != value)
                    {
                        _mbytHashType = value;
                        _mstrOriginalString = String.Empty;
                        _mstrHashString = String.Empty;

                        this.SetEncryptor();
                    }
                }
            }

            public string SaltValue
            {
                get { return _mstrSaltValue; }
                set { _mstrSaltValue = value; }
            }

            public bool UseSalt
            {
                get { return _mboolUseSalt; }
                set { _mboolUseSalt = value; }
            }

            public short SaltLength
            {
                get { return _msrtSaltLength; }
                set { _msrtSaltLength = value; }
            }

            public string OriginalString
            {
                get { return _mstrOriginalString; }
                set { _mstrOriginalString = value; }
            }

            public string HashString
            {
                get { return _mstrHashString; }
                set { _mstrHashString = value; }
            }
            #endregion

            #region "Constructors"
            public PdsaHash()
            {
                _mbytHashType = PdsaHashType.MD5;
            }

            public PdsaHash(PdsaHashType HashType)
            {
                _mbytHashType = HashType;
            }

            public PdsaHash(PdsaHashType HashType,
              string OriginalString)
            {
                _mbytHashType = HashType;
                _mstrOriginalString = OriginalString;
            }

            public PdsaHash(PdsaHashType HashType,
              string OriginalString,
              bool UseSalt,
              string SaltValue)
            {
                _mbytHashType = HashType;
                _mstrOriginalString = OriginalString;
                _mboolUseSalt = UseSalt;
                _mstrSaltValue = SaltValue;
            }

            #endregion

            #region "SetEncryptor() Method"

            private void SetEncryptor()
            {
                switch (_mbytHashType)
                {
                    case PdsaHashType.MD5:
                        _mhash = new MD5CryptoServiceProvider();
                        break;
                    case PdsaHashType.SHA1:
                        _mhash = new SHA1CryptoServiceProvider();
                        break;
                    case PdsaHashType.SHA256:
                        _mhash = new SHA256Managed();
                        break;
                    case PdsaHashType.SHA384:
                        _mhash = new SHA384Managed();
                        break;
                    case PdsaHashType.SHA512:
                        _mhash = new SHA512Managed();
                        break;
                }
            }
            #endregion

            #region "CreateHash() Methods"
            public string CreateHash()
            {
                byte[] bytValue;
                byte[] bytHash;

                // Create New Crypto Service Provider Object
                this.SetEncryptor();

                // Check to see if we will Salt the value
                if (_mboolUseSalt)
                    if (_mstrSaltValue.Length == 0)
                        _mstrSaltValue = this.CreateSalt();

                // Convert the original string to array of Bytes
                bytValue =
                  System.Text.Encoding.UTF8.GetBytes(
                  _mstrSaltValue + _mstrOriginalString);

                // Compute the Hash, returns an array of Bytes  
                bytHash = _mhash.ComputeHash(bytValue);

                // Return a base 64 encoded string of the Hash value
                return Convert.ToBase64String(bytHash);
            }

            public string CreateHash(string OriginalString)
            {
                _mstrOriginalString = OriginalString;
                return this.CreateHash();
            }

            public string CreateHash(string OriginalString,
              PdsaHashType HashType)
            {
                _mstrOriginalString = OriginalString;
                _mbytHashType = HashType;
                return this.CreateHash();
            }

            public string CreateHash(string OriginalString,
              bool UseSalt)
            {
                _mstrOriginalString = OriginalString;
                _mboolUseSalt = UseSalt;
                return this.CreateHash();
            }

            public string CreateHash(string OriginalString,
              PdsaHashType HashType,
              string SaltValue)
            {
                _mstrOriginalString = OriginalString;
                _mbytHashType = HashType;
                _mstrSaltValue = SaltValue;
                return this.CreateHash();
            }

            public string CreateHash(string OriginalString,
              string SaltValue)
            {
                _mstrOriginalString = OriginalString;
                _mstrSaltValue = SaltValue;
                return this.CreateHash();
            }
            #endregion


            public void Reset()
            {
                _mstrSaltValue = String.Empty;
                _mstrOriginalString = String.Empty;
                _mstrHashString = String.Empty;
                _mboolUseSalt = false;
                _mbytHashType = PdsaHashType.MD5;
                _mhash = null;
            }

            public string CreateSalt()
            {
                byte[] bytSalt = new byte[8];
                RNGCryptoServiceProvider rng;
                rng = new RNGCryptoServiceProvider();
                rng.GetBytes(bytSalt);
                return Convert.ToBase64String(bytSalt);
            }
        }
    }
