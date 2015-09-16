using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Library
{
    public static class LanguageFilter
    {

        public static String Filter(String inputText){
            List<String> offensiveWords = new List<string>();
            offensiveWords.Add("nigga");
            offensiveWords.Add("nigger");
            offensiveWords.Add("shit");
            offensiveWords.Add("fuck");
            offensiveWords.Add("puss");
            offensiveWords.Add("dick");
            offensiveWords.Add("bitch");




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