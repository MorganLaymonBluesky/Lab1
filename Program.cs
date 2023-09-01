using Lab1;

// Initialization

string rootFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
string filePath = "";
double nintendoPercentage = 0.0;
double adventurePercentage = 0.0;
double userPublisherPercentage = 0.0;
double userGenrePercentage = 0.0;
string userPublisher = "";
string userGenre = "";

List<VideoGame> vgList = new List<VideoGame>();
IEnumerable<VideoGame> nintendoList = new List<VideoGame>();
IEnumerable<VideoGame> adventureList = new List<VideoGame>();
IEnumerable<VideoGame> userGenreList = new List<VideoGame>();
IEnumerable<VideoGame> userPublisherList = new List<VideoGame>();

// Execute

InputFile();
vgList.Sort();

/* Here is the code for making a list of Nintendo Games and Adventure Games, but since the code seemed redundant to include, 
 * (since the same functions are used in the methods below) I put it into a comment.

    nintendoList = vgList.ToList();
    nintendoList = nintendoList.Where(vg => vg.publisher.ToUpper() == "NINTENDO");
    nintendoPercentage = Math.Round(Convert.ToDouble(nintendoList.Count()) / Convert.ToDouble(vgList.Count()), 4);
    Console.WriteLine("Out of " + vgList.Count() + " games, " + nintendoList.Count() + " have been developed by Nintendo, which is " + nintendoPercentage * 100 + "%. \n");

    adventureList = vgList.ToList();
    adventureList = adventureList.Where(vg => vg.genre.ToUpper() == "ADVENTURE");
    adventurePercentage = Math.Round(Convert.ToDouble(adventureList.Count()) / Convert.ToDouble(vgList.Count()), 4);
    Console.WriteLine("Out of " + vgList.Count() + " games, " + adventureList.Count() + " are adventure titles, which is " + adventurePercentage * 100 + "%.\n");
*/

Console.WriteLine("\nPlease enter a Publisher that you would like to see statistics for: ");
userPublisher = Console.ReadLine();
while (string.IsNullOrWhiteSpace(userPublisher))
{
    Console.WriteLine("That is not a valid input. Please enter a valid Publisher: ");
    userPublisher = Console.ReadLine();
}
PublisherData(userPublisher);

Console.WriteLine("Please enter a Genre that you would like to see statistics for: ");
userGenre = Console.ReadLine();
while (string.IsNullOrWhiteSpace(userGenre))
{
    Console.WriteLine("That is not a valid input. Please enter a valid Genre: ");
    userGenre = Console.ReadLine();
}
GenreData(userGenre);

void InputFile()
{
    Console.WriteLine("Please enter the name of the file you would like to open: (Example: FileName.csv)\nNote: This file must be held within the same folder as the Project Files.");
    var parseFileName = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(parseFileName))
    {
        Console.WriteLine("Error: Invalid File Directory. Please try again.");
        parseFileName = Console.ReadLine();
    }
    filePath = rootFolder + Path.DirectorySeparatorChar + parseFileName;
    try
    {
        using (StreamReader newReader = new StreamReader(filePath))
        {
            List<string> lines = new List<string>();
            while (!newReader.EndOfStream)
            {
                lines.Add(newReader.ReadLine());
            }
            for (int i = 1; i < lines.Count; i++)
            {
                string nextVG = lines[i];
                string[] vgProperties = nextVG.Split(',');

                VideoGame newVG = new VideoGame(vgProperties[0], vgProperties[1], Convert.ToInt32(vgProperties[2]), vgProperties[3], vgProperties[4], Convert.ToDouble(vgProperties[5]), Convert.ToDouble(vgProperties[6]), Convert.ToDouble(vgProperties[7]), Convert.ToDouble(vgProperties[8]), Convert.ToDouble(vgProperties[9]));
                vgList.Add(newVG);
            }
            newReader.Close();
        }
        Console.WriteLine("Success! File: " + parseFileName + " found.");
    }
    catch
    {
        Console.WriteLine("Error: File not found.");
    }
}

void PublisherData(string publisher)
{
    userPublisherList = vgList.ToList();
    userPublisherList = userPublisherList.Where(vg => vg.publisher.ToUpper() == publisher.ToUpper());
    while (userPublisherList.Count() == 0)
    {
        Console.WriteLine("This Publisher is not contained within this list.\nPlease try again: ");
        publisher = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(publisher))
        {
            Console.WriteLine("That is not a valid input. Please enter a valid Publisher: ");
            publisher = Console.ReadLine();
        }
        userPublisherList = userPublisherList.Where(vg => vg.publisher.ToUpper() == publisher.ToUpper());
    }
    userPublisherPercentage = Math.Round(Convert.ToDouble(userPublisherList.Count()) / Convert.ToDouble(vgList.Count()), 4);
    foreach (VideoGame vg in userPublisherList)
    {
        Console.WriteLine(vg);
    }
    Console.WriteLine("Out of " + vgList.Count() + " games, " + userPublisherList.Count() + " have been developed by " + publisher + ", which is " + userPublisherPercentage * 100 + "%. \n");
}

void GenreData(string genre)
{
    userGenreList = vgList.ToList();
    userGenreList = userGenreList.Where(vg => vg.genre.ToUpper() == genre.ToUpper());
    while (userGenreList.Count() == 0)
    {
        Console.WriteLine("This Genre is not contained within this list.\nPlease try again: ");
        genre = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(genre))
        {
            Console.WriteLine("That is not a valid input. Please enter a valid Genre: ");
            genre = Console.ReadLine();
        }
        userGenreList = userGenreList.Where(vg => vg.genre.ToUpper() == genre.ToUpper());
    }
    foreach (VideoGame vg in userGenreList)
    {
        Console.WriteLine(vg);
    }
    userGenrePercentage = Math.Round(Convert.ToDouble(userGenreList.Count()) / Convert.ToDouble(vgList.Count()), 4);
    Console.WriteLine("Out of " + vgList.Count() + " games, " + userGenreList.Count() + " are developed by " + genre + ", which is " + userGenrePercentage * 100 + "%. \n");
}
