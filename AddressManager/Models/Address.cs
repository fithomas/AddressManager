
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

        public Address(string firstname, string lastname, string street, string city, string zip)
        {
            Firstname = firstname;
            Lastname = lastname;
            Street = street;
            City = city;
            /* add: Zip / PLZ setzen */
            Zip = zip;
        }

        #endregion

        #region properties

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Street { get; set; }

        /* add: Zip / Plz Property hinzufügen */
        public string Zip { get; set; }

        public string City { get; set; }

        #endregion
    }
}
