﻿namespace Machete.X12Schema.V5010
{
    using X12;


    public interface L2330C_837D :
        X12Layout
    {
        Segment<NM1> ReferringProvider { get; }
        
        SegmentList<REF> SecondaryIdentification { get; }
    }
}