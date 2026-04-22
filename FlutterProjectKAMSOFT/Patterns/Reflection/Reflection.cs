using System.Reflection;

namespace FlutterProjectKAMSOFT.Patterns.Reflection
{
    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }   
    public class ReflectionExample
    {
        public void DisplayProperties(object obj)
        {
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                Console.WriteLine($"{prop.Name}: {value}");
            }
        }
    }
}
