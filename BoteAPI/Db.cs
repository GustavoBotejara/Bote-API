namespace BoteApi.DB;

public record Bote 
{
    public int Id { get; set; }
    public string ? Name { get; set; }
}

public class BoteDB 
{
    private static List<Bote> _bote = new List<Bote>()
    {
        new Bote{ Id =1, Name="Bote 1"},
        new Bote{ Id =2, Name="Bote 2"},
        new Bote{ Id =3, Name="Bote 3"}
    };

    public static List<Bote> GetBotes()
    {
        return _bote;
    }

    public static Bote ? GetBote(int id) 
    {
        return _bote.SingleOrDefault(bote => bote.Id == id);
    }

    public static Bote CreateBote(Bote bote) 
    {
        _bote.Add(bote);
        return bote;
    }

    public static Bote UpdateBote(Bote boteUpdate)
    {
        _bote = _bote.Select(bote => 
        {
            if(bote.Id == boteUpdate.Id) 
            {
                bote.Name = boteUpdate.Name;
            }
            return bote;
        }).ToList();
        return boteUpdate;
    }

    public static void DeleteBote(int id)
    {
        _bote = _bote.FindAll(bote => bote.Id != id).ToList();
    }
}