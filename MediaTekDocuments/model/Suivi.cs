using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Suivi
    {
        public int Id { get; }
        public string Etat { get; }

        public Suivi(int id, string etat)
        {
            this.Id = id;
            this.Etat = etat;
        }

        /// <summary>
        /// Récupération du libellé des etats de la commmandes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Etat;
        }
    }
}
