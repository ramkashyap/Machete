// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// ORL_O34_SPECIMEN (GroupMap) - 
    /// </summary>
    public class ORL_O34_SPECIMENMap :
        HL7TemplateMap<ORL_O34_SPECIMEN>
    {
        public ORL_O34_SPECIMENMap()
        {
            Segment(x => x.SPM, 0, x => x.Required = true);
            Segments(x => x.OBX, 1);
            Segments(x => x.SAC, 2);
            Groups(x => x.Order, 3);
        }
    }
}