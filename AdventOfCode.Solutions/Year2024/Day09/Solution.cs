namespace AdventOfCode.Solutions.Year2024.Day09;

class Solution : SolutionBase
{
    public Solution() : base(09, 2024, "Disk Fragmenter", false) { }

    protected override string SolvePartOne()
    {
        var disk = new Disk(Input);
        disk.Defragment();
        return disk.Checksum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }


}

class Disk
{
    public long?[] FileIds { get; private set; }
    public Disk(string data)
    {
        var blockSize = data.Select(c => c).Sum(c => long.Parse(c.ToString()));
        FileIds = new long?[blockSize];
        bool isFileLength = false;
        bool isFreeSpace = false;
        var position = 0;
        var fileId = 0;
        for (int i = 0; i < data.Length; i++)
        {
            var item = int.Parse(data[i].ToString());
            isFileLength = i % 2 == 0;
            isFreeSpace = i % 2 != 0;
            if (isFileLength)
            {
                for (int x = 0; x < item; x++)
                {
                    FileIds[position++] = fileId;
                }
                fileId++;
            }
            else if (isFreeSpace)
            {
                for (int x = 0; x < item; x++)
                {
                    FileIds[position++] = null;
                }
            }
        }
    }

    public void Defragment()
    {
        for (int i = FileIds.Length - 1; i >= 0; i--)
        {
            if (FileIds[i] is null)
            {
                continue;
            }
            for (int x = 0; x < i; x++)
            {
                if (FileIds[x] is null)
                {
                    FileIds[x] = FileIds[i];
                    FileIds[i] = null;
                    break;
                }
            }
        }
    }

    public void DefragmentFullFileId()
    {
        for (int i = FileIds.Length - 1; i >= 0; i--)
        {
            if (FileIds[i] is null)
            {
                continue;
            }
            for (int x = 0; x < i; x++)
            {
                if (FileIds[x] is null)
                {
                    FileIds[x] = FileIds[i];
                    FileIds[i] = null;
                    break;
                }
            }
        }
    }

    public long Checksum()
    {
        long checksum = 0;
        var fileId = 0;
        for (int i = 0; i < FileIds.Length; i++)
        {
            if (FileIds[i] is not null)
            {
                checksum += fileId * FileIds[i]!.Value;
                fileId++;
            }
        }
        return checksum;
    }
}
