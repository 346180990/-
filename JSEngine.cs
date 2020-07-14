using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SpiderMusic.model;
using Nancy.Json;
using System.Collections;
using System.Linq;

namespace SpiderMusic
{
    class JSEngine
    {
        public static returnValue getJSEngine() {
            var phone = new phone
            {
                Phone = "13201661424",
                Password = "asdliuyang123",
                Countrycode = "86",
                RememberLogin = "true"
            };
            var commit = new commit 
            {
                Csrf_token="",
                Cursor= "-1",
                Offset= "0",
                OrderType= "1",
                PageNo= "1",
                PageSize= "20",
                Rid= "A_PL_0_924680166",
                ThreadId= "A_PL_0_924680166"
            };
            JavaScriptSerializer json = new JavaScriptSerializer();
            string phonejson = json.Serialize(commit);
            //获取本地core地址
            string ScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "core.js");
            //获取js引擎实例
            var switcher = JsEngineSwitcher.Current;
            switcher.EngineFactories.Add(new ChakraCoreJsEngineFactory());
            switcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;
            IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine();
            engine.ExecuteFile(ScriptPath, Encoding.UTF8);
            var returnValue = new returnValue();
            try
            {
                string[] s = engine.CallFunction("myFunc", phonejson).ToString().Split(",,,");
                returnValue.Params = s[0];
                returnValue.encSecKey = s[1];
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
            return returnValue;
        }
    }
}
