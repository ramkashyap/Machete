﻿namespace Machete.X12Schema.V5010.Maps
{
    using X12;
    using X12.Configuration;


    public class L2330H_837IMap :
        X12LayoutMap<L2330H_837I, X12Entity>
    {
        public L2330H_837IMap()
        {
            Id = "2330H";
            Name = "Other Payer Referring Provider";
            
            Segment(x => x.ReferringProvider, 0);
            Segment(x => x.SecondaryIdentification, 1, x => x.IsRequired());
        }
    }
}