using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Library
{
    static public class WordLink
    {
        static public String CreateLinks(String input) {
            List<String> commonWords = new List<string>();
            commonWords.Add("the");
            commonWords.Add("in");
            commonWords.Add("and");
            commonWords.Add("with");
            commonWords.Add("a");
            commonWords.Add("an");
            commonWords.Add("i");
            commonWords.Add("i'm");
            commonWords.Add("you");
            commonWords.Add("to");
            commonWords.Add("but");
            commonWords.Add("get");
            commonWords.Add("your");
            commonWords.Add("it");
            commonWords.Add("is");
            commonWords.Add("it's");
            commonWords.Add("for");
            commonWords.Add("on");
            commonWords.Add("i'm");
            commonWords.Add("so");
            commonWords.Add("they");
            commonWords.Add("have");
            commonWords.Add("that's");
            commonWords.Add("that");
            commonWords.Add("should");
            commonWords.Add("or");
            commonWords.Add("like");
            commonWords.Add("when");

            String output="";
            String[] wordArray = input.Split(' ');
            foreach(var word in wordArray){
                Boolean blnAdd = true;
                foreach (String commonWord in commonWords)
                {
                    if (commonWord.ToLower() == word.ToLower())
                    {
                        blnAdd = false;
                        break;
                    }
                }
                if (blnAdd)
                {
                    output += "<span class=\"" + word + " word\">" + word + "</span> ";
                }
                else {
                    output += word + " ";
                }
                
            }
            return output.Trim();
        }
    }
}