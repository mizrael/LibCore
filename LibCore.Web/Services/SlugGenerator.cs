using System.Text.RegularExpressions;

namespace LibCore.Web.Services
{
    public class SlugGenerator : ISlugGenerator
    {
        private readonly int _maxLength;

        public SlugGenerator(int maxLength)
        {
            _maxLength = maxLength;
        }

        public string GenerateSlug(string phrase)
        {
            var str = phrase.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            if (_maxLength > 0)
                str = str.Substring(0, str.Length <= _maxLength ? str.Length : _maxLength).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
    }
}
