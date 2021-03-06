﻿namespace Machete.X12Schema.V5010.Maps
{
    using X12;
    using X12.Configuration;


    public class L2330C_837IMap :
        X12LayoutMap<L2330C_837I, X12Entity>
    {
        public L2330C_837IMap()
        {
            Id = "2330C";
            Name = "Other Payer Operating Physician";
            
            Segment(x => x.AttendingProvider, 0);
            Segment(x => x.SecondaryIdentification, 1, x => x.IsRequired());
        }
    }
}