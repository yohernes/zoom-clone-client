using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace zoom_client
{
    public static class DataProcessor
    {
        
        public static readonly string[] countries = new string[]
            {
                "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Argentina", "Armenia", "Australia",
                "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
                "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei",
                "Bulgaria", "Burkina Faso", "Burundi", "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Central African Republic",
                "Chad", "Chile", "China", "Colombia", "Comoros", "Congo", "Costa Rica", "Croatia", "Cuba", "Cyprus",
                "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador",
                "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini", "Ethiopia", "Fiji", "Finland", "France", "Gabon",
                "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana",
                "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy",
                "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea", "Kosovo", "Kuwait", "Kyrgyzstan",
                "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
                "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius",
                "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique", "Myanmar",
                "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Macedonia",
                "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
                "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russia", "Rwanda", "Saint Kitts and Nevis",
                "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia",
                "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
                "Somalia", "South Africa", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Sweden", "Switzerland",
                "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tonga", "Trinidad and Tobago",
                "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom",
                "United States", "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia",
                "Zimbabwe"
            };
    
        public static byte[] ResizeAndCompressImage(Image image, int width, int height, long quality)
        {
            using (Bitmap resizedBitmap = new Bitmap(image, new Size(width, height)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                    resizedBitmap.Save(ms, jpegCodec, encoderParams);
                    return ms.ToArray();
                }
            }
        }
        public static bool IsEmail(string email)
        {
            string strRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(email))
                return (true);
            MessageBox.Show("invalid email");
            return false;
        }
        public static bool IsUsername(string username)
        {
            
             // Define the regular expression pattern
            string pattern = @"^[A-Za-z0-9]{3,12}$";

             // Create a Regex object with the specified pattern
            Regex regex = new Regex(pattern);

                // Check if the input string matches the pattern
            if( regex.IsMatch(username)) return (true);
            MessageBox.Show("invalid username");
            return false;


        }
        public static bool IsPassword(string password)
        {
            
            
                // Define the regular expression pattern
                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,}$";

                // Create a Regex object with the specified pattern
                Regex regex = new Regex(pattern);

            // Check if the input string matches the pattern
            if (regex.IsMatch(password)) 
            {
                return true;
            }
            MessageBox.Show("invalid password");
            return false;

        }
        public static bool IsFirstOLastName(string name)
        {
            // Define the regular expression pattern
            string pattern = @"^[A-Za-z]{1,11}$";

            // Create a Regex object with the specified pattern
            Regex regex = new Regex(pattern);

            // Check if the input string matches the pattern
            if( regex.IsMatch(name))
                return true;
            MessageBox.Show("invalid first or last name");
            return false;
        }
        
        public static byte[] AddMetadata(byte[] frameData, string metatype)
        {
            byte[] metadata = Encoding.UTF8.GetBytes($"%%%{metatype}%%%|");
            byte[] result = new byte[metadata.Length + frameData.Length];

            Buffer.BlockCopy(metadata, 0, result, 0, metadata.Length);
            Buffer.BlockCopy(frameData, 0, result, metadata.Length, frameData.Length);

            return result;
        }
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
    }
   

}
