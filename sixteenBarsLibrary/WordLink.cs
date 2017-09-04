﻿using System;
using System.Collections.Generic;

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
            commonWords.Add("be");
            commonWords.Add("if");
            commonWords.Add("she");
            commonWords.Add("he");
            commonWords.Add("it");
            commonWords.Add("do");
            commonWords.Add("don't");
            commonWords.Add("does");
            commonWords.Add("doesn't");
            commonWords.Add("all");
            commonWords.Add("of");
            commonWords.Add("me");
            commonWords.Add("no");
            commonWords.Add("my");
            commonWords.Add("will");
            commonWords.Add("take");
            commonWords.Add("took");
            commonWords.Add("he's");
            commonWords.Add("she's");
            commonWords.Add("what");
            commonWords.Add("what's");
            commonWords.Add("was");

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
                    output += "<span class=\"" + word.Replace(",", "").Replace(".", "").Replace("?", "").Replace("!", "") + " word\">" + word + "</span> ";
                }
                else {
                    output += word + " ";
                }
                
            }
            return output.Trim();
        }
    }
}