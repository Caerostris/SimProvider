using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimProvider.Graphics
{
    public class ContentManager<T>
    {
        private Dictionary<string, T> data = new Dictionary<string,T>();
        public string dataPath = "Data/Textures";
        private LoadContentFunction<T> load;

        public ContentManager(string dataPath, LoadContentFunction<T> lcf){
            this.dataPath = dataPath;
            this.load = lcf;
        }

        public void add(string name, T val)
        {
            if (data.Keys.Contains(name))
                data[name] = val;
            else
                data.Add(name, val);
        }
        public T get(string name)
        {
            if (!data.ContainsKey(name))
            {
                try
                {
                    add(name,load(name, dataPath));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(name + " konnte nicht geladen werden!");
                    Console.WriteLine(ex.Message);
                    return default(T);
                }
                return data[name];
            }
            else
                return data[name];
        }
    }
    public delegate T LoadContentFunction<T>(string name, string dataPath);
}
