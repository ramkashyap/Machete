// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// PPG_PCG_PROBLEM (GroupMap) - 
    /// </summary>
    public class PPG_PCG_PROBLEMMap :
        HL7TemplateMap<PPG_PCG_PROBLEM>
    {
        public PPG_PCG_PROBLEMMap()
        {
            Segment(x => x.PRB, 0, x => x.Required = true);
            Segments(x => x.NTE, 1);
            Segments(x => x.VAR, 2);
            Groups(x => x.ProblemRole, 3);
            Groups(x => x.ProblemObservation, 4);
        }
    }
}