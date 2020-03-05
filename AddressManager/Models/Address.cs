
//-----------------------------------------------------------------------


// <copyright file="Address.cs" company="Siemens">


//     Copyright (c) Siemens. All rights reserved.
//     Author: Noah Eggenberger
//     Date: 05.09.2019
//     Description: Model for an Address

// </copyright>


//-----------------------------------------------------------------------

namespace AddressManager.Models
{
    public class Address
    {
        #region constructor

        public Address() { }

        public Address(string firstname, string lastname, string street, string city)
        {
            Firstname = firstname;
            Lastname = lastname;
            Street = street;
            City = city;
            /* ToDo: Zip / PLZ setzen */ 
        }

        #endregion

        #region properties

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Street { get; set; }

        /* ToDo: Zip / Plz Property hinzufügen */

        public string City { get; set; }

        #endregion
    }
}
