using System.Text;

namespace DemoApp.Service
{
    public class CaptchaService
    {
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string? _currentCaptcha;

        public static string GenerateCaptcha(int length = 5)
        {
            var random = new Random();
            var captcha = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                captcha.Append(chars[random.Next(chars.Length)]);
            }

            _currentCaptcha = captcha.ToString();
            return _currentCaptcha;
        }

        public static bool ValidateCaptcha(string input)
        {
            return !string.IsNullOrEmpty(_currentCaptcha) &&
                   string.Equals(_currentCaptcha, input, StringComparison.OrdinalIgnoreCase);
        }
    }
}
