using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Library
{
    public class LanguageFilter
    {
        List<String> offensiveWords = new List<string>();
        public LanguageFilter() {
            offensiveWords.Add("nigga");
            offensiveWords.Add("nigger");
            offensiveWords.Add("shit");
            offensiveWords.Add("fuck");
            offensiveWords.Add("puss");
            offensiveWords.Add("dick");
            offensiveWords.Add("bitch");
        }

        public String Filter(String inputText){
            String output;
            output = inputText;
            foreach (String word in offensiveWords) {
                String replacement = word.Substring(0, 1);
                foreach (char c in word)
                {
                    replacement += "*";
                }
                replacement = replacement.Remove(word.Length - 1, 1);

                output = output.Replace(word, replacement);
            }


            return output;
        }
    }
}