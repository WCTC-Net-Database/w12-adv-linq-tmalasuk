namespace ConsoleRpg.Helpers;

public class OutputManager
{
    private readonly List<(string message, ConsoleColor color)> _outputBuffer; // A list of messages with associated colors

    public OutputManager()
    {
        _outputBuffer = new List<(string message, ConsoleColor color)>();
    }

    public void Clear()
    {
        Console.Clear();
        _outputBuffer.Clear();
    }

    public void Display()
    {
        foreach (var (message, color) in _outputBuffer)
        {
            WriteColorToConsole(message, color); // Write stored messages with color
        }

        _outputBuffer.Clear(); // Clear the buffer after displaying
    }

    public void DisplayError(string message)
    {
        Clear();
        _outputBuffer.Add((message, ConsoleColor.Red));
        Display();
        Thread.Sleep(1500);

    }

    public void DisplayErrorBelow(string message, int topLine)
    {
        ClearBelow(topLine);
        _outputBuffer.Add((message, ConsoleColor.Red));
        Display();
        Thread.Sleep(2500);
    }

    public void WriteandDisplay(string message, ConsoleColor color = ConsoleColor.White)
    {
        _outputBuffer.Add((message, color));
        Display();
    }

    public void Write(string message, ConsoleColor color = ConsoleColor.White)
    {
        _outputBuffer.Add((message, color));
    }

    public void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        _outputBuffer.Add((message + Environment.NewLine, color));
    }

    private void WriteColorToConsole(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public void ClearBelow(int topLine)
    {
        int currentTop = Console.GetCursorPosition().Top;
        int totalLines = Console.WindowHeight;

        for (int i = topLine; i < totalLines; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, topLine);
    }


    public string[] BoxPanel(string[] lines)
    {
        // Remove trailing spaces from each line BEFORE measuring width
        var trimmed = lines.Select(l => l.TrimEnd()).ToArray();

        int maxWidth = trimmed.Max(line => line.Length);
        string border = "=" + new string('=', maxWidth + 2) + "=";

        string[] boxed = new string[trimmed.Length + 2];
        boxed[0] = border;

        for (int i = 0; i < trimmed.Length; i++)
        {
            boxed[i + 1] = "| " + trimmed[i].PadRight(maxWidth) + " |";
        }

        boxed[boxed.Length - 1] = border;

        return boxed;
    }


    public string[] CraftMenu(string[] options, int height, string menuTitle, int menuWidth = 0)
    {
        List<string> lines = new List<string>();

        // Determine dynamic width if caller didn't supply one
        int longestOption = options.Length > 0
            ? options.Max(o => $"[X] {o}   ".Length)
            : 0;

        int minimumContentWidth = Math.Max(longestOption, menuTitle.Length) + 4;
        // +4 for padding inside the borders

        // Actual width = provided width OR auto width
        int width = Math.Max(menuWidth, minimumContentWidth);

        // Add a blank line at the top
        lines.Add("".PadRight(width - 4));

        // Add numbered options
        for (int i = 0; i < options.Length; i++)
        {
            string optionText = $"[{i + 1}] {options[i]}";
            lines.Add(optionText.PadRight(width - 4));
        }

        // Fill remaining height with empty lines
        int totalInnerLines = height - 2;
        while (lines.Count < totalInnerLines)
            lines.Add("".PadRight(width - 4));

        // Build dynamic title border
        int titlePadding = (width - menuTitle.Length - 2) / 2;
        int extra = (width - menuTitle.Length - 2) % 2;

        string topBorder =
            $"{new string('=', titlePadding)} {menuTitle} {new string('=', titlePadding + extra)}";

        string bottomBorder = new string('=', width);

        // Wrap lines
        List<string> boxedLines = new List<string>();
        boxedLines.Add(topBorder);

        foreach (var line in lines)
            boxedLines.Add($"| {line} |");

        boxedLines.Add(bottomBorder);

        return boxedLines.ToArray();
    }




    public int PrintSideBySide(string[] left, string[] right, int separation)
    {
        int offset = 1; // left margin
        int leftWidth = left.Max(l => l.Length);
        int rightWidth = right.Max(l => l.Length);

        int maxLines = Math.Max(left.Length, right.Length);
        int lastPrintedWidth = 0;

        for (int i = 0; i < maxLines; i++)
        {
            string leftString =
                i < left.Length
                ? new string(' ', offset) + left[i].PadRight(leftWidth)
                : new string(' ', offset + leftWidth);

            string rightString =
                i < right.Length
                ? right[i]
                : "";

            // dynamic separation between panels
            string fullLine = leftString + new string(' ', separation) + rightString;

            lastPrintedWidth = fullLine.Length;

            WriteLine(fullLine);
        }

        Display();
        return lastPrintedWidth;
    }

    public List<string> WrapText(string text, int width)
    {
        List<string> lines = new List<string>();
        string[] words = text.Split(' ');

        string current = "";

        foreach (var word in words)
        {
            if ((current + word).Length > width)
            {
                lines.Add(current.TrimEnd());
                current = "";
            }
            current += word + " ";
        }

        if (current.Length > 0)
            lines.Add(current.TrimEnd());

        return lines;
    }


}
