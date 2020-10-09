using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace CsvCode
{
    public class CsvDataframe
    {
        private List<string> DataFrame;

        public CsvDataframe()
        {
            DataFrame = null;
        }

        public CsvDataframe(string[] arr)
        {
            List<string> temp = new List<string>(arr);
            DataFrame = temp;
        }

        public CsvDataframe(List<string> arr)
        {
            DataFrame = arr;
        }

        //GET
        public string[] getRow(int row)
        {
            string[] temp = DataFrame[row].Split(',');
            return temp;
        }

        public string getElement(int row, int column)
        {
            string[] temp = getRow(row);
            return temp[column];
        }

        //Convert dataframe to tables then return the contents of a column
        //Slow, use sparringly

        public string[] getCell(int column)
        {
            List<List<string>> table = new List<List<string>>();
            List<string> output = new List<string>();

            foreach(string s in DataFrame)
            {
                List<string> temp = new List<string>();
                temp.AddRange(StringToArray(s));
                table.Add(temp);
            }
            
            for(int i=0; i < table.Count(); i++)
            {
                output.Add(table[i][column]);
            }

            return output.ToArray();
        }

        public int getDataframeSize()
        {
            return DataFrame.Count();
        }

        public List<string> getDataframe()
        {
            return DataFrame;
        }
        
        // CONVERSION FUNCTIONS
        public string[] ListToArray()
        {
            string[] temp = DataFrame.ToArray();
            return temp;
        }

        public string[] ListToArray(List<string> input)
        {
            string[] temp = input.ToArray();
            return temp;
        }

        public string ArrayToString(string[] input)
        {
            string s = string.Join(",", input);
            return s;
        }

        public string[] StringToArray(string input)
        {
            string[] temp = input.Split(',');
            return temp;
        }

        // READER

        public List<string> LoadFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            List<string> temp = new List<string>(lines);
            DataFrame = temp;
            return DataFrame;
        }

        //WRITER

        public void AppendToCsv(string filename)
        {
            if ((filename != null) && (DataFrame != null))
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (string s in DataFrame)
                    {
                        writer.WriteLine(s);
                    }
                    writer.Close();
                }
                Console.WriteLine(String.Format("Saved successfully at {0}", filename));
            }
            else
                Console.WriteLine("ERROR: No data saved, dataframe is empty or file path is unreachable");
        }

        // FUNCTIONS
        // Param: Input to append your array/string to the bottom of the list
        public void Append(string input)
        {
            if(DataFrame == null)
            {
                List<string> temp = new List<string>();
                temp.Add(input);
                DataFrame = temp;
            }

            DataFrame.Add(input);
        }

        public void Append(string[] input)
        {
            if (DataFrame == null)
            {
                List<string> temp = new List<string>();
                temp.AddRange(input);
                DataFrame = temp;
            }

            DataFrame.AddRange(input);
        }

        // INSERT
        // Insert in a specific position
        // Param: Pos = Position in list to insert the input, Input = Input (array or string)
        
        public void IndexInsert(int pos, string input)
        {
            DataFrame.Insert(pos, input);
        }


        public void IndexInsert(int pos, string[] input)
        {
            DataFrame.InsertRange(pos, input);
        }

        // EDIT
        // Replace specific column/index
        // Param: Row = the row index, column = column index (starting from 0), input = Input that'll overrwrite current data
        public void IndexEdit(int row, string input)
        {
            DataFrame[row] = input;
        }

        public void IndexEdit(int row, int column, string input)
        {
            string s = DataFrame[row];
            string[] temp = s.Split(',');
            temp[column] = input;
            string s2 = string.Join(",", temp);
            DataFrame[row] = s2;
        }

        // DELETE
        // Delete a specific entry
        // Param: row = row index to be deleted
        public void DeleteEntry(int row)
        {
            DataFrame.RemoveAt(row);
        }

        static void Main(string[] args)
        {
            // USAGE TUTORIAL:

            //You can place an array inside the object to initialize it with an array
            //If you leave it empty it's just null

            //string[] test = { "Num, Action, Pos1, Pos2, Pos3, Pos4","1, Jogging, 1235123.4, 163, 23123, 24124",
            //   "2, Walking, 162, 1235123.4, 23123, 123145", "3, Fapping, 552123, 231234, 59592, 4123" };

            //CsvDataframe df = new CsvDataframe(test);

            //df.AppendToCsv("B:\\Downloads\\test_best.csv");

            //CsvDataframe df2 = new CsvDataframe();
            //You can load a csv file like so (It's  in the form a list)
            //List<string> test2 = df2.LoadFile("B:\\Downloads\\test_best.csv");

            //If you need contents as an array (or use the prebuilt list to array function lol)
            //string[] example = df2.ListToArray();

            //You can append a single line or an array
            //string[] appendTest = { "4, Idle, 5124, 5325, 123, 555", "5, Run, 5124, 5325, 123, 555" };
            //df.Append(appendTest);

            //IndexInsert to Insert according to the index
            //df.IndexInsert(6, "6, Walk, 25123, 3232, 5352, 23131");

            //You can also insert an array
            //string[] insertTest = { "7, Idle, 2342, 23123, 55, 232", "8,Fap,234124,3232,5123,532" };
            //df.IndexInsert(7, insertTest);

            //Edit a specific column
            //df.IndexEdit(2,1,"skjaskdjlsa");

            //Edit a specific row
            //df.IndexEdit(2, "2, Walking, 162, 1235123.4, 23123, 123145");

            //Delete a row
            //df.DeleteEntry(2);

            //ArrayToString Conversion
            //string[] testa = { "1", "Running", "235235", "234134", "325235", "2352351" };
            //Console.WriteLine(df.ArrayToString(testa));

            //Get  function tests
            //-------------------
            //string[] temp = df.getRow(2);
            //string temp = df.getColumn(2, 1);
            //Console.WriteLine(df.getDataframeSize());

            /*
            //Print out Data
            for (int i = 0; i < df.DataFrame.Count; i++)
            {
                Console.WriteLine(df.DataFrame[i]);
            }
            */

            //Save again to file
            //df.AppendToCsv("B:\\Downloads\\test_best.csv");

            /* 1 MILLION DATA TEST
             * DELETE AFTER

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            CsvDataframe df = new CsvDataframe();
            for(int i = 0; i < 1000000; i++)
            {
                List<string> s = new List<string>();

                for(int j = 0; j < 5; j++)
                {
                    s.Add(String.Format("Insert {0}", j));
                }
                string[] temp = df.ListToArray(s);
                string x = df.ArrayToString(temp);

                df.Append(x);
            }

            df.AppendToCsv("B:\\Downloads\\1mtest.csv");

            watch.Stop();
            Console.WriteLine("Elapsed Time for Writing: " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            df.LoadFile("B:\\Downloads\\1mtest.csv");
            watch.Stop();
            Console.WriteLine("Elapsed Time for Reading: " + watch.ElapsedMilliseconds + "ms");
            */

            //Get cell
            var watch = new System.Diagnostics.Stopwatch();
            CsvDataframe csv = new CsvDataframe();
            csv.LoadFile("B:\\Downloads\\test_best.csv");

            watch.Start();
            string[] temp = csv.getCell(1);
            watch.Stop();
            
            foreach (string s in temp)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("Elapsed time for get cell function: " + watch.ElapsedMilliseconds + "ms");
        }
    }
}
