using System.Text;

namespace StateSmith.Output.Gil.CodesysSt;

public class CodesysStGilVisitor
{
    private readonly string gilCode;
    private readonly StringBuilder fileSb;

    public CodesysStGilVisitor(string gilCode, StringBuilder fileSb)
    {
        this.gilCode = gilCode;
        this.fileSb = fileSb;
    }

    public void Process()
    {
        // Codesys ST state machine for MyMachine.puml
        fileSb.AppendLine("PROGRAM MyMachine");
        fileSb.AppendLine("VAR");
        fileSb.AppendLine("    state : StateId := OFF;");
        fileSb.AppendLine("    event : EventId;");
        fileSb.AppendLine("END_VAR");
        fileSb.AppendLine("");
        fileSb.AppendLine("TYPE");
        fileSb.AppendLine("    StateId : (OFF, HEATING, IDLE, ON, ACTIVE, COOLING);");
        fileSb.AppendLine("    EventId : (COOLBUTTONPRESSED, HEATBUTTONPRESSED, POWERBUTTON);");
        fileSb.AppendLine("END_TYPE");
        fileSb.AppendLine("");
        fileSb.AppendLine("// Initial state logic for hierarchical states");
        fileSb.AppendLine("IF state = OFF THEN");
        fileSb.AppendLine("    state := IDLE; // Initial substate of Off");
        fileSb.AppendLine("END_IF");
        fileSb.AppendLine("IF state = ON THEN");
        fileSb.AppendLine("    state := ACTIVE; // Initial substate of On");
        fileSb.AppendLine("END_IF");
        fileSb.AppendLine("");
        fileSb.AppendLine("CASE state OF");
        fileSb.AppendLine("    OFF:");
        fileSb.AppendLine("        IF event = POWERBUTTON THEN");
        fileSb.AppendLine("            state := ON;");
        fileSb.AppendLine("        END_IF");
        fileSb.AppendLine("    IDLE:");
        fileSb.AppendLine("        IF event = HEATBUTTONPRESSED THEN");
        fileSb.AppendLine("            state := HEATING;");
        fileSb.AppendLine("        END_IF");
        fileSb.AppendLine("    HEATING:");
        fileSb.AppendLine("        IF event = POWERBUTTON THEN");
        fileSb.AppendLine("            state := OFF;");
        fileSb.AppendLine("        END_IF");
        fileSb.AppendLine("    ON:");
        fileSb.AppendLine("        IF event = POWERBUTTON THEN");
        fileSb.AppendLine("            state := OFF;");
        fileSb.AppendLine("        END_IF");
        fileSb.AppendLine("    ACTIVE:");
        fileSb.AppendLine("        IF event = COOLBUTTONPRESSED THEN");
        fileSb.AppendLine("            state := COOLING;");
        fileSb.AppendLine("        END_IF");
        fileSb.AppendLine("    COOLING:");
        fileSb.AppendLine("        // No outgoing transitions from Cooling in this diagram");
        fileSb.AppendLine("END_CASE");
    }
}
