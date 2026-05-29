using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models;

namespace ProxyFlyweightBridgeDecorator.Models.Factories
{
    public class CharacterFactory
    {
        private readonly Dictionary<string, Character> _characters = new();

        public Character GetCharacter(char symbol, string font, int size)
        {
            string key = $"{symbol}-{font}-{size}";

            if(!_characters.ContainsKey(key))
            {
                _characters[key] = new Character(symbol, font, size);
            }

            return _characters[key];
        }

        public int GetCharacterCount()
        {
            return _characters.Count;
        }
    }
}
