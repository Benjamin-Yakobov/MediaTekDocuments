using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System;
using Newtonsoft.Json;


namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }


        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Récupération de toutes les commandes d'un livre.
        /// </summary>
        /// <param name="idLivre"></param>
        /// <returns></returns>
        public List<CommandeDocument> GetCommandesLivre(string idLivre)
        {
            return access.GetCommandesLivres(idLivre);
        }

        /// <summary>
        /// Récupération de tous les états possibles d'une commande.
        /// </summary>
        /// <returns></returns>
        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        /// <summary>
        /// Ajout d'une commande passée en paramètre.
        /// </summary>
        /// <param name="commande"></param>
        public void AjouterCommande(CommandeDocument commande)
        {
            AjouterModifierSupprimerCommande(commande.Id, commande.DateCommande, commande.Montant, commande.NbExemplaire, commande.IdLivreDvd, commande.IdSuivi, commande.Etat, "ajouter");
        }

        /// <summary>
        /// Modification d'une commande passée en paramètre.
        /// </summary>
        /// <param name="commande"></param>
        public void ModifierCommande(CommandeDocument commande)
        {
            AjouterModifierSupprimerCommande(commande.Id, commande.DateCommande, commande.Montant, commande.NbExemplaire, commande.IdLivreDvd, commande.IdSuivi, commande.Etat, "modifier");

        }

        /// <summary>
        /// Suppression d'une commande passée en paramètre.
        /// </summary>
        /// <param name="commande"></param>
        public void SupprimerCommande(CommandeDocument commande)
        {
            AjouterModifierSupprimerCommande(commande.Id, commande.DateCommande, commande.Montant, commande.NbExemplaire, commande.IdLivreDvd, commande.IdSuivi, commande.Etat, "supprimer");

        }

        /// <summary>
        /// Ajouter, Modifier, Supprimer, une commande.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="nbExemplaire"></param>
        /// <param name="idLivreDvd"></param>
        /// <param name="idSuivi"></param>
        /// <param name="etat"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool AjouterModifierSupprimerCommande(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, int idSuivi, string etat, string action)
        {
            Dictionary<string, object> commandeLivre = new Dictionary<string, object>();
            commandeLivre.Add("Id", id);
            commandeLivre.Add("DateCommande", dateCommande.ToString("yyyy-MM-dd"));
            commandeLivre.Add("Montant", montant);
            commandeLivre.Add("NbExemplaire", nbExemplaire);
            commandeLivre.Add("IdLivreDvd", idLivreDvd);
            commandeLivre.Add("IdSuivi", idSuivi);
            commandeLivre.Add("Etat", etat);

            if(action == "ajouter")
            {
                access.AjouterEnregistrement("commandedocument", JsonConvert.SerializeObject(commandeLivre));
            }
            else
            {
                if(action == "modifier")
                {
                    access.ModifierEnregistrement("commandedocument", id, JsonConvert.SerializeObject(commandeLivre));
                }
                else
                {
                    access.SupprimerEnregistrement("commandedocument", JsonConvert.SerializeObject(commandeLivre));
                }
            }

            return false;
        }

        /// <summary>
        /// Récuperation de l'id maximal des commandes.
        /// </summary>
        public string GetMaxIdCommande()
        {
            string maxId = access.getMaxId("maxcommande");
            return maxId;
        }
    }

}
