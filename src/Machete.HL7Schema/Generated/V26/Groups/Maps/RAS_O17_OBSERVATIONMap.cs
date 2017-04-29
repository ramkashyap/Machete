// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// RAS_O17_OBSERVATION (GroupMap) - 
    /// </summary>
    public class RAS_O17_OBSERVATIONMap :
        HL7TemplateMap<RAS_O17_OBSERVATION>
    {
        public RAS_O17_OBSERVATIONMap()
        {
            Segment(x => x.OBX, 0, x => x.Required = true);
            Segments(x => x.NTE, 1);
        }
    }
}