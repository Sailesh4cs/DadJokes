using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dad_Jokes.Models
{
    public class RandomJoke
    {
        public bool Sucess { get; set; }
        public List<RandomJokeBody> body { get; set; }
    }

    public class RandomJokeBody
    {
        public string Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
        public int JokeCOunt { get; set; }
    }
    public class JokeCount
    {
        public bool Sucess { get; set; }
        public int body { get; set; }
    }
}
