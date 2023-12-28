namespace Subjects.LeetCode;

public class ReverseString
{
    private readonly List<char> _input;

    public ReverseString(List<char> input)
    {
        _input = input;
    }

    public List<char> SolutionA()
    {
        _input.Reverse();
        return _input;
    }
    
    public List<char> SolutionB()
    {
        for (int i = 0; i < _input.Count; i++)
        {
            var asd = _input[0];
            _input.RemoveAt(0);
            _input.Add(asd);
        }

        return _input;
    }
    
    
    public List<char> SolutionC(int place = 1)
    {
        if (place == _input.Count) return _input;
        _input.Add(_input[0]);
        _input.RemoveAt(0);
        return SolutionC(place + 1);
    }

}