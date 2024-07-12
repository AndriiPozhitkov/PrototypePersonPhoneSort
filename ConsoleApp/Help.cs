namespace App;

public class Help
{
    public static string Text => @"
help

person phone external merge sort

.\ppsort [command] <input_file> [output_file]

[command]:

  sort - by default, sort an <input_file> into [output_file]
  scan - collect lines statistics of an <input_file>
  help - print out current help information

examples:

.\ppsort persons.csv
.\ppsort sort persons.csv sorted_persons.csv
.\ppsort scan persons.csv
.\ppsort help
";
}