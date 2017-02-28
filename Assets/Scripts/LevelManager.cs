using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class LevelManager
{
    #region GameLevels
    public static Level GetLevel01()
    {
        Level lvl = Level.Create(rows: 3, cols: 1, levelID: 1, bronzeTarget: 7, silverTarget: 5, goldTarget: 3);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(2, 0, Level.ColourType.Red);
        lvl.AddMovable(0, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(2, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(1, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;        
    }

    public static Level GetLevel02()
    {
        Level lvl = Level.Create(rows: 4, cols: 1, levelID: 2, bronzeTarget: 9, silverTarget: 7, goldTarget: 5);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(3, 0, Level.ColourType.Red);
        lvl.AddMovable(0, 0, Level.ColourType.Blue);
        lvl.AddMovable(1, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(2, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(3, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(1, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel03()
    {
        Level lvl = Level.Create(rows: 5, cols: 1, levelID: 3, bronzeTarget: 14, silverTarget: 12, goldTarget: 10);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(4, 0, Level.ColourType.Red);
        lvl.AddMovable(3, 0, Level.ColourType.Red);
        lvl.AddMovable(0, 0, Level.ColourType.Blue);
        lvl.AddMovable(1, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(1, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(4, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(3, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel04()
    {
        Level lvl = Level.Create(rows: 4, cols: 3, levelID: 4, bronzeTarget: 13, silverTarget: 11, goldTarget: 9);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(0, 1, Level.ColourType.Red);
        lvl.AddMovable(1, 1, Level.ColourType.Red);
        lvl.AddMovable(2, 1, Level.ColourType.Blue);
        lvl.AddMovable(3, 1, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(2, 1, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(3, 1, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(0, 1, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(1, 1, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        lvl.ChangeTile(2, 2, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel05()
    {
        Level lvl = Level.Create(rows: 3, cols: 3, levelID: 5, bronzeTarget: 11, silverTarget: 9, goldTarget: 7);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(0, 0, Level.ColourType.Red);
        lvl.AddMovable(2, 1, Level.ColourType.Blue);
        lvl.AddMovable(2, 2, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(2, 2, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(1, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(2, 1, Level.TileType.Landing);
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel06()
    {
        Level lvl = Level.Create(rows: 5, cols: 3, levelID: 6, bronzeTarget: 12, silverTarget: 10, goldTarget: 8);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(0, 0, Level.ColourType.Red);
        lvl.AddMovable(4, 0, Level.ColourType.Blue);
        lvl.AddMovable(2, 2, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(2, 2, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(1, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        lvl.ChangeTile(3, 0, Level.TileType.Landing);
        lvl.ChangeTile(4, 0, Level.TileType.Landing);
        lvl.ChangeTile(2, 1, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel07()
    {
        Level lvl = Level.Create(rows: 7, cols: 1, levelID: 7, bronzeTarget: 15, silverTarget: 13, goldTarget: 11);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(0, 0, Level.ColourType.Red);
        lvl.AddMovable(4, 0, Level.ColourType.Blue);
        lvl.AddMovable(5, 0, Level.ColourType.Blue);
        lvl.AddMovable(6, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(6, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(1, 0, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(2, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(3, 0, Level.TileType.Landing);
        lvl.ChangeTile(4, 0, Level.TileType.Landing);
        lvl.ChangeTile(5, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel08()
    {
        Level lvl = Level.Create(rows: 5, cols: 1, levelID: 8, bronzeTarget: 9, silverTarget: 7, goldTarget: 5);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(4, 0, Level.ColourType.Red);
        lvl.AddMovable(0, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(4, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(1, 0, Level.TileType.Landing);
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel09()
    {
        Level lvl = Level.Create(rows: 4, cols: 2, levelID: 9, bronzeTarget: 10, silverTarget: 8, goldTarget: 6);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(3, 0, Level.ColourType.Red);
        lvl.AddMovable(0, 0, Level.ColourType.Blue);
        //Desintations
        lvl.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(3, 0, Level.TileType.Destination, Level.ColourType.Blue);
        //Landing pads
        lvl.ChangeTile(2, 0, Level.TileType.Landing);
        lvl.ChangeTile(1, 1, Level.TileType.Landing);
        lvl.ChangeTile(2, 1, Level.TileType.Landing);
        lvl.ChangeTile(3, 1, Level.TileType.Landing);
        lvl.ChangeTile(0, 1, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel10()
    {
        Level lvl = Level.Create(rows: 5, cols: 5, levelID: 10, bronzeTarget: 14, silverTarget: 12, goldTarget: 11);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(0, 2, Level.ColourType.Red);
        lvl.AddMovable(2, 0, Level.ColourType.Blue);
        lvl.AddMovable(4, 2, Level.ColourType.Yellow);
        lvl.AddMovable(2, 4, Level.ColourType.Green);
        //Desintations
        lvl.ChangeTile(2, 0, Level.TileType.Destination, Level.ColourType.Red);
        lvl.ChangeTile(2, 4, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(0, 2, Level.TileType.Destination, Level.ColourType.Yellow);
        lvl.ChangeTile(4, 2, Level.TileType.Destination, Level.ColourType.Green);
        //Landing pads
        lvl.ChangeTile(0, 0, Level.TileType.Landing);
        lvl.ChangeTile(0, 1, Level.TileType.Landing);
        lvl.ChangeTile(1, 0, Level.TileType.Landing);
        lvl.ChangeTile(1, 2, Level.TileType.Landing);
        lvl.ChangeTile(2, 1, Level.TileType.Landing);
        lvl.ChangeTile(2, 2, Level.TileType.Landing);
        //Flower pads
        //logs
        //disappearing logs
        //rocks
        //fountains
        //birds

        return lvl;
    }

    public static Level GetLevel11()
    {
        Level lvl = Level.Create(rows: 7, cols: 7, levelID: 11, name: "Bonus", bronzeTarget: 100, silverTarget: 100, goldTarget: 100);//TODO placeholder, complete all levels properly

        //Frogs
        lvl.AddMovable(2, 2, Level.ColourType.Green);
        lvl.AddMovable(2, 4, Level.ColourType.Blue);
        lvl.AddMovable(4, 2, Level.ColourType.Red);
        lvl.AddMovable(4, 4, Level.ColourType.Yellow);


        //0
        lvl.ChangeTile(0, 0, Level.TileType.Redirect, 1, 0);
        lvl.ChangeTile(0, 1, Level.TileType.Redirect, 0, -1);
        lvl.ChangeTile(0, 2, Level.TileType.Landing);
        lvl.ChangeTile(0, 3, Level.TileType.Landing);
        lvl.ChangeTile(0, 4, Level.TileType.Landing);
        lvl.ChangeTile(0, 5, Level.TileType.NonLanding);
        lvl.ChangeTile(0, 6, Level.TileType.Obstacle);

        //1
        lvl.ChangeTile(1, 0, Level.TileType.Redirect, 1, 0);
        lvl.ChangeTile(1, 1, Level.TileType.Destination, Level.ColourType.Blue);
        lvl.ChangeTile(1, 2, Level.TileType.FakeDisappearing);
        lvl.ChangeTile(1, 3, Level.TileType.Disappearing);
        lvl.ChangeTile(1, 4, Level.TileType.FakeDisappearing);
        lvl.ChangeTile(1, 5, Level.TileType.Enemy);
        lvl.ChangeTile(1, 6, Level.TileType.Obstacle);

        //2
        lvl.ChangeTile(2, 0, Level.TileType.Redirect, 0, 1);
        lvl.ChangeTile(2, 1, Level.TileType.Redirect, 0, 1);
        lvl.ChangeTile(2, 2, Level.TileType.Landing);
        lvl.ChangeTile(2, 3, Level.TileType.Redirect, 0, 1);
        lvl.ChangeTile(2, 4, Level.TileType.Landing);
        lvl.ChangeTile(2, 5, Level.TileType.Landing);
        lvl.ChangeTile(2, 6, Level.TileType.NonLanding);

        //3
        lvl.ChangeTile(3, 0, Level.TileType.Redirect, 1, 0);
        lvl.ChangeTile(3, 1, Level.TileType.Redirect, 0, -1);
        lvl.ChangeTile(3, 2, Level.TileType.Redirect, 0, -1);
        lvl.ChangeTile(3, 3, Level.TileType.NonLanding);
        lvl.ChangeTile(3, 4, Level.TileType.Redirect, -1, 0);
        lvl.ChangeTile(3, 5, Level.TileType.Enemy);
        lvl.ChangeTile(3, 6, Level.TileType.Landing);

        //4
        lvl.ChangeTile(4, 0, Level.TileType.Landing);
        lvl.ChangeTile(4, 1, Level.TileType.Enemy);
        lvl.ChangeTile(4, 2, Level.TileType.Landing);
        lvl.ChangeTile(4, 3, Level.TileType.Redirect, 0, -1);
        lvl.ChangeTile(4, 4, Level.TileType.Landing);
        lvl.ChangeTile(4, 5, Level.TileType.Disappearing);
        lvl.ChangeTile(4, 6, Level.TileType.Obstacle);

        //5
        //lvl.ChangeTile(5, 0, Level.TileType.
        lvl.ChangeTile(5, 1, Level.TileType.Destination, Level.ColourType.Yellow);
        lvl.ChangeTile(5, 2, Level.TileType.Disappearing);
        lvl.ChangeTile(5, 3, Level.TileType.NonLanding);
        lvl.ChangeTile(5, 4, Level.TileType.Obstacle);
        lvl.ChangeTile(5, 5, Level.TileType.Destination, Level.ColourType.Green);
        lvl.ChangeTile(5, 6, Level.TileType.Landing);

        //6
        lvl.ChangeTile(6, 0, Level.TileType.FakeDisappearing);
        lvl.ChangeTile(6, 1, Level.TileType.Landing);
        lvl.ChangeTile(6, 2, Level.TileType.Landing);
        lvl.ChangeTile(6, 3, Level.TileType.FakeDisappearing);
        lvl.ChangeTile(6, 4, Level.TileType.FakeDisappearing);
        lvl.ChangeTile(6, 5, Level.TileType.Disappearing);
        lvl.ChangeTile(6, 6, Level.TileType.FakeDisappearing);

        return lvl;
    }

    #endregion


    public static List<Level> GetAllLevels()
    {
        MethodInfo[] mis = typeof(LevelManager).GetMethods().Where(m=> m.Name.StartsWith("GetLevel")).OrderBy(mm=>mm.Name).ToArray();

        List<Level> lvls = new List<Level>();

        foreach (MethodInfo mi in mis)
        {
            lvls.Add((Level)mi.Invoke(null, new object[] { }));
        }

        return lvls;
    }



    #region StaticLevels
    public static Level GetDemoLevel()
    {
        //Demo Level
        Level v1 = Level.Create(3, 5);
        v1.BronzeTarget = 11;
        v1.SilverTarget = 9;
        v1.GoldTarget = 7;

        v1.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Blue);
        v1.AddMovable(0, 0, Level.ColourType.Red);
        v1.ChangeTile(0, 1, Level.TileType.Landing);
        v1.ChangeTile(0, 2, Level.TileType.Landing);
        v1.ChangeTile(0, 3, Level.TileType.Landing);
        v1.ChangeTile(1, 1, Level.TileType.Landing);
        v1.ChangeTile(1, 3, Level.TileType.Landing);
        v1.ChangeTile(0, 4, Level.TileType.Destination, Level.ColourType.Red);
        v1.AddMovable(0, 4, Level.ColourType.Blue);
        v1.ChangeTile(1, 2, Level.TileType.Enemy);


        v1.ChangeTile(2, 0, Level.TileType.Landing);
        v1.ChangeTile(2, 4, Level.TileType.Landing);

        return v1;
    }

    public static Level GetTestLevel()
    {
        Level v1 = Level.Create(20, 5);
        //0
        v1.ChangeTile(0, 0, Level.TileType.Landing);
        v1.AddMovable(0, 0, Level.ColourType.Blue);
        v1.ChangeTile(0, 2, Level.TileType.Landing);
        v1.ChangeTile(0, 4, Level.TileType.Obstacle);

        //1
        v1.ChangeTile(1, 0, Level.TileType.Landing);
        v1.AddMovable(1, 0, Level.ColourType.Yellow);
        v1.ChangeTile(1, 1, Level.TileType.Landing);
        v1.AddMovable(1, 1, Level.ColourType.Red);
        v1.ChangeTile(1, 2, Level.TileType.Landing);

        //2
        v1.ChangeTile(2, 0, Level.TileType.Landing);
        v1.AddMovable(2, 0, Level.ColourType.Green);
        v1.ChangeTile(2, 1, Level.TileType.FakeDisappearing);
        v1.ChangeTile(2, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(2, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(2, 4, Level.TileType.Obstacle);

        //3
        v1.ChangeTile(3, 0, Level.TileType.Landing);
        v1.AddMovable(3, 0, Level.ColourType.Red);
        v1.ChangeTile(3, 1, Level.TileType.NonLanding);
        v1.ChangeTile(3, 2, Level.TileType.Landing);
        v1.ChangeTile(3, 3, Level.TileType.Enemy);
        v1.ChangeTile(3, 4, Level.TileType.Landing);

        //4
        v1.ChangeTile(4, 0, Level.TileType.Landing);
        v1.AddMovable(4, 0, Level.ColourType.Blue);
        v1.ChangeTile(4, 1, Level.TileType.Disappearing);

        //5
        v1.ChangeTile(5, 0, Level.TileType.Landing);
        v1.AddMovable(5, 0, Level.ColourType.Blue);
        v1.ChangeTile(5, 1, Level.TileType.Obstacle);

        //6
        v1.ChangeTile(6, 0, Level.TileType.Landing);
        v1.AddMovable(6, 0, Level.ColourType.Green);
        v1.ChangeTile(6, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(6, 2, Level.TileType.Landing);

        //7
        v1.ChangeTile(7, 0, Level.TileType.Landing);
        v1.AddMovable(7, 0, Level.ColourType.Red);
        v1.ChangeTile(7, 1, Level.TileType.Enemy);

        //8
        v1.ChangeTile(8, 0, Level.TileType.Landing);
        v1.AddMovable(8, 0, Level.ColourType.Yellow);
        v1.ChangeTile(8, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(8, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(8, 3, Level.TileType.Disappearing);

        //9
        v1.ChangeTile(9, 0, Level.TileType.Landing);
        v1.AddMovable(9, 0, Level.ColourType.Blue);
        v1.ChangeTile(9, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(9, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(9, 4, Level.TileType.NonLanding);

        //10
        v1.ChangeTile(10, 0, Level.TileType.Landing);
        v1.AddMovable(10, 0, Level.ColourType.Green);
        v1.ChangeTile(10, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(10, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(10, 4, Level.TileType.Disappearing);

        //11
        v1.ChangeTile(11, 0, Level.TileType.Landing);
        v1.AddMovable(11, 0, Level.ColourType.Red);
        v1.ChangeTile(11, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(11, 2, Level.TileType.Landing);
        v1.AddMovable(11, 2, Level.ColourType.Green);
        v1.ChangeTile(11, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(11, 4, Level.TileType.Enemy);

        //12
        v1.ChangeTile(12, 0, Level.TileType.Landing);
        v1.AddMovable(12, 0, Level.ColourType.Yellow);
        v1.ChangeTile(12, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(12, 2, Level.TileType.Landing);
        v1.AddMovable(12, 2, Level.ColourType.Blue);
        v1.ChangeTile(12, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(12, 4, Level.TileType.FakeDisappearing);

        //13
        v1.ChangeTile(13, 0, Level.TileType.Landing);
        v1.AddMovable(13, 0, Level.ColourType.Blue);
        v1.ChangeTile(13, 2, Level.TileType.NonLanding);

        //14
        v1.ChangeTile(14, 0, Level.TileType.Landing);
        v1.AddMovable(14, 0, Level.ColourType.Green);
        v1.ChangeTile(14, 1, Level.TileType.Landing);
        v1.AddMovable(14, 1, Level.ColourType.Red);
        v1.ChangeTile(14, 2, Level.TileType.Disappearing);

        //15
        v1.ChangeTile(15, 0, Level.TileType.Landing);
        v1.AddMovable(15, 0, Level.ColourType.Yellow);
        v1.ChangeTile(15, 1, Level.TileType.Enemy);
        v1.ChangeTile(15, 2, Level.TileType.Disappearing);

        //16
        v1.ChangeTile(16, 0, Level.TileType.Landing);
        v1.AddMovable(16, 0, Level.ColourType.Blue);
        v1.ChangeTile(16, 1, Level.TileType.Enemy);
        v1.ChangeTile(16, 2, Level.TileType.Redirect, 0, 1);

        //17
        v1.ChangeTile(17, 0, Level.TileType.Landing);
        v1.AddMovable(17, 0, Level.ColourType.Green);

        //18
        v1.ChangeTile(18, 0, Level.TileType.Landing);
        v1.AddMovable(18, 0, Level.ColourType.Red);
        v1.ChangeTile(18, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(18, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(18, 4, Level.TileType.Redirect, 1, 0);
        v1.ChangeTile(19, 4, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 3, Level.TileType.NonLanding);
        v1.ChangeTile(19, 2, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 1, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 0, Level.TileType.Redirect, -1, 0);

        return v1;
    }
    #endregion


    public static Level GenerateBlankLevel(int rows, int cols)
    {
        Level lvl = Level.Create(rows, cols);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                lvl.ChangeTile(r, c, Level.TileType.Landing);
            }
        }
        return lvl;
    }

}






////TODO Restore below file save/load code rather than in code levels
//public class LevelManager
//{
//    private static bool isEncrypted = false;
//    public static bool IsEncrypted { get { return isEncrypted; } set { isEncrypted = value; } }

//    private static readonly string encryptionKey = "FrOgGyStYle2016";//encryption password


//    public static Level RestoreLevel(string path)
//    {
//        if (!File.Exists(path))
//            throw new FileNotFoundException("Could not find level file: " + path);

//        string xml = File.ReadAllText(path);//get level data from file

//        if (IsEncrypted)
//            xml = Decrypt(xml);

//        Level v;
//        try
//        {
//            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(xml));//push string into stream for deserialising
//            XmlSerializer ser = new XmlSerializer(typeof(Level));
//            v = (Level)ser.Deserialize(ms);
//        }
//        catch (Exception e)
//        {
//            throw new LevelReadException(xml, e);
//        }

//        return v;
//    }

//    public static void SaveLevel(Level v, string path)
//    {
//        string xml;
//        try
//        {
//            XmlSerializer ser = new XmlSerializer(typeof(Level));
//            MemoryStream ms = new MemoryStream();//xml serialise into memory stream
//            ser.Serialize(ms, v);
//            xml = Encoding.ASCII.GetString(ms.ToArray());
//        }
//        catch (Exception e)
//        {
//            throw new LevelWriteException(v, e);
//        }

//        if (IsEncrypted)
//            xml = Encrypt(xml);

//        if (!Directory.Exists(path))
//            Directory.CreateDirectory(Path.GetDirectoryName(path));

//        File.WriteAllText(path, xml);
//    }


//    /// <summary>
//    /// Adds AES string encryption on the raw data file to stop tampering
//    /// </summary>
//    /// <param name="clearText">raw data to encrypt</param>
//    /// <returns>encyrpted string</returns>
//    private static string Encrypt(string clearText)
//    {
//        string EncryptionKey = encryptionKey;
//        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
//        using (Aes encryptor = Aes.Create())
//        {
//            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
//            encryptor.Key = pdb.GetBytes(32);
//            encryptor.IV = pdb.GetBytes(16);
//            using (MemoryStream ms = new MemoryStream())
//            {
//                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
//                {
//                    cs.Write(clearBytes, 0, clearBytes.Length);
//                    cs.Close();
//                }
//                clearText = Convert.ToBase64String(ms.ToArray());
//            }
//        }
//        return clearText;
//    }

//    /// <summary>
//    /// Removes AES encryption from a string to reveal data structure
//    /// </summary>
//    /// <param name="cipherText">encrypted text</param>
//    /// <returns>decrypted data file</returns>
//    private static string Decrypt(string cipherText)
//    {
//        string EncryptionKey = encryptionKey;
//        cipherText = cipherText.Replace(" ", "+");
//        byte[] cipherBytes = Convert.FromBase64String(cipherText);
//        using (Aes encryptor = Aes.Create())
//        {
//            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
//            encryptor.Key = pdb.GetBytes(32);
//            encryptor.IV = pdb.GetBytes(16);
//            using (MemoryStream ms = new MemoryStream())
//            {
//                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
//                {
//                    cs.Write(cipherBytes, 0, cipherBytes.Length);
//                    cs.Close();
//                }
//                cipherText = Encoding.Unicode.GetString(ms.ToArray());
//            }
//        }
//        return cipherText;
//    }





//    public class LevelReadException : Exception
//    {
//        public string JSON { get; private set; }
//        public LevelReadException(string json, Exception x) : base("Failed to parse from JSON", x)
//        {
//            JSON = json;
//        }
//    }
//    public class LevelWriteException : Exception
//    {
//        public Level Level { get; private set; }
//        public LevelWriteException(Level v, Exception x) : base("Failed to convert to JSON", x)
//        {
//            Level = v;
//        }
//    }




//    //// Use this for initialization
//    //void Start () {

//    //}

//    //// Update is called once per frame
//    //void Update () {

//    //}
//}
