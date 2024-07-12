using Bogus;
using Bogus.DataSets;

namespace ExternalMergeSort.Test;

public class GenerateTestFile
{
    private const char FieldDelimiter = ';';
    private const long FileMaxSizeByte = 4 * 1024; // 4_294_967_296;
    private const string TempDir = "C:\\Temp";

    //[Fact]
    public void Generate()
    {
        var faker = new Faker<PersonPhone>()
            .CustomInstantiator(PersonPhone.Fake);

        var personsFileName = $"persons_{DateTime.Now.Ticks:X}.csv";
        using var w = new StreamWriter(Path.Combine(TempDir, personsFileName));
        while (w.BaseStream.Length < FileMaxSizeByte)
        {
            faker.Generate().Write(w);
        }
    }

    private record PersonPhone(string LastName, string FirstName, string MiddleName, string Phone)
    {
        internal static PersonPhone Fake(Faker f) => new(
            f.Person.LastName,
            f.Person.FirstName,
            f.Name.FirstName(Name.Gender.Male),
            f.Person.Phone);

        internal void Write(StreamWriter w)
        {
            w.Write(LastName);
            w.Write(FieldDelimiter);
            w.Write(FirstName);
            w.Write(FieldDelimiter);
            w.Write(MiddleName);
            w.Write(FieldDelimiter);
            w.WriteLine(Phone);
        }
    };
}