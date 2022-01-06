using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Nicknames
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public int CharacterLimit { get; set; } = 100;
    }
}
