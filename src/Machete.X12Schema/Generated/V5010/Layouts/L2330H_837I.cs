﻿namespace Machete.X12Schema.V5010.Layouts
{
    using X12;


    public interface L2330H_837I :
        X12Layout
    {
        Segment<NM1> ReferringProvider { get; }
        
        SegmentList<REF> SecondaryIdentification { get; }
    }
}