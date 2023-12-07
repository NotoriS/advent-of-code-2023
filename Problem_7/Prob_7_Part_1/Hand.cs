using System;
using System.Collections;

public class Hand : IComparable<Hand>
{
    private List<char> cards;

    public int Bid { get; private set; }

    private Dictionary<char, int> cardRankLookup = new Dictionary<char, int>
    {
        { '2', 1 },
        { '3', 2 },
        { '4', 3 },
        { '5', 4 },
        { '6', 5 },
        { '7', 6 },
        { '8', 7 },
        { '9', 8 },
        { 'T', 9 },
        { 'J', 10 },
        { 'Q', 11 },
        { 'K', 12 },
        { 'A', 13 },
    };

    private enum EHandRank 
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6,
    }

    private EHandRank Rank
    {
        get 
        {
            Dictionary<char, int> cardToCount = new Dictionary<char, int>();

            foreach (char card in cards)
            {
                if (cardToCount.ContainsKey(card))
                {
                    cardToCount[card]++;
                }
                else
                {
                    cardToCount.Add(card, 1);
                }
            }

            List<int> duplicateCounts = cardToCount.Values.ToList();
            duplicateCounts.Sort();

            if (duplicateCounts.Count == 5)
            {
                return EHandRank.HighCard;
            }

            if (duplicateCounts.Count == 4)
            {
                return EHandRank.OnePair;
            }

            if (duplicateCounts.Count == 3 && duplicateCounts[2] == 2)
            {
                return EHandRank.TwoPair;
            }

            if (duplicateCounts.Count == 3 && duplicateCounts[2] == 3)
            {
                return EHandRank.ThreeOfAKind;
            }

            if (duplicateCounts.Count == 2 && duplicateCounts[1] == 3)
            {
                return EHandRank.FullHouse;
            }

            if  (duplicateCounts.Count == 2 && duplicateCounts[1] == 4) 
            {
                return EHandRank.FourOfAKind;
            }

            return EHandRank.FiveOfAKind;
        }
    }

    public Hand(List<char> cards, int bid)
    {
        if (cards.Count != 5)
        {
            throw new ArgumentException("The card list passed into a hand must contain exactly five cards.");
        }

        this.cards = cards;
        this.Bid = bid;
    }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException("Tried to compare to a null reference");
        }

        if (this.Rank != other.Rank)
        {
            return this.Rank > other.Rank ? 1 : -1;
        }

        for (int i = 0; i < 5; i++)
        {
            if (cardRankLookup[this.cards[i]] > cardRankLookup[other.cards[i]]) return 1;
            if (cardRankLookup[this.cards[i]] < cardRankLookup[other.cards[i]]) return -1;
        }

        return 0;
    }
}
