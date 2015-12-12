﻿using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _12_2 {
    class Program {
        static int sum = 0;

        static void Main(string[] args) {
            StreamReader file = new StreamReader("input.txt");
            JsonTextReader reader = new JsonTextReader(file);
            JArray json = (JArray)JToken.ReadFrom(reader);
            file.Close();

            EvalJson(json);

            Console.WriteLine(sum);
            Console.ReadLine();
        }

        static void EvalJson(JToken token) {
            if(token.HasValues)
                foreach(JToken child in token.Children()) {
                    if(child.Type == JTokenType.Object) {
                        bool valid = true;
                        foreach(var property in (JObject)child)
                            if(property.Value.ToString() == "red")
                                valid = false;
                        if(valid)
                            EvalJson(child);
                    }
                    else
                        EvalJson(child);
                }
            else if(token.Type == JTokenType.Integer)
                sum += token.Value<int>();
        }
    }
}
