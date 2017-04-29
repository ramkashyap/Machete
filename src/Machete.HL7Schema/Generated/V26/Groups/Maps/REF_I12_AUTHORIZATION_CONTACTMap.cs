// This file was automatically generated and may be regenerated at any
// time. To ensure any changes are retained, modify the tool with any segment/component/group/field name
// or type changes.
namespace Machete.HL7Schema.V26.Maps
{
    using V26;

    /// <summary>
    /// REF_I12_AUTHORIZATION_CONTACT (GroupMap) - 
    /// </summary>
    public class REF_I12_AUTHORIZATION_CONTACTMap :
        HL7TemplateMap<REF_I12_AUTHORIZATION_CONTACT>
    {
        public REF_I12_AUTHORIZATION_CONTACTMap()
        {
            Segment(x => x.AUT, 0, x => x.Required = true);
            Segment(x => x.CTD, 1);
        }
    }
}