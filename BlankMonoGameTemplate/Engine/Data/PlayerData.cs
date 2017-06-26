using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate
{
    public sealed class PlayerData
    {
        #region SimpleSingleton

        private PlayerData() { }
        private static PlayerData _instance = null;
        public static PlayerData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerData();
                }
                return _instance;
            }
        }

        #endregion

        public int Keys { get; set; }
    }
}
