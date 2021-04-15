using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BrickGame
{
    public class Score
    {
        public string PlayerName { get; set; }

        public int Value { get; set; }
    }


    public class ScoreManager
    {
        private static string _fileName = "scoreboard.xml";

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
            :this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);
            Scores = Scores.OrderBy(c => c.Value).ToList(); // Order list
            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            // Create a new file, if it doesn't exist
            if (!File.Exists(_fileName))
            {
                return new ScoreManager();
            }

            // Otherwise, load the file
            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));
                var scores = (List<Score>)serializer.Deserialize(reader);
                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(6).ToList(); // Takes the first 6 elements
        }

        public static void Save(ScoreManager scoreManger)
        {
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));
                serializer.Serialize(writer, scoreManger.Scores);
            }
        }
    }
}
