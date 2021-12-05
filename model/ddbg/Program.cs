using System;


class Program
{
    static void Main(string[] args)
    {
    	Random random = new Random();

    	Match match = new Match(
    		new BigMoneyAgent(),
    		new BigMoneyAgent(),
    		Presets.MakeStandardMatchType(),
            random
    	);

    	while(!match.NextRound())
    	{
            Console.WriteLine("completed round " + match.CurrentRound);
    	}

    	Console.WriteLine("game terminated after " + match.CurrentRound + " rounds");

    }
}

