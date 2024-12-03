using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Interpreter : MonoBehaviour
{
    Dictionary<string, string> colors = new Dictionary<string, string>()
    {
        {"orange", "#ef5847"},
        {"yellow" , "#f2f1b9"}
    };

    List<string> response = new List<string>();
    private bool awaitingScanResponse = false;
    private bool awaitingTerminateResponse = false;
    private bool virusTerminated = false; 

    public async Task<List<string>> Interpret(string userInput)
    {
        response.Clear();
        string lowerInput = userInput.ToLower().Trim();


        if (lowerInput == "exit")
        {
            response.Add("Exiting the system. Goodbye!");
            UnityEditor.EditorApplication.isPlaying = false; 
            Application.Quit(); 
            return response;
        }

        if (awaitingTerminateResponse)
        {
            if (lowerInput == "yes")
            {
                response.Add("Terminate Done!");
                virusTerminated = true; 
                awaitingTerminateResponse = false; 
            }
            else if (lowerInput == "no")
            {
                response.Add("Termination aborted.");
                awaitingTerminateResponse = false;
            }
            else
            {
                response.Add("Invalid response. Please answer with 'yes' or 'no'.");
            }

            return response;
        }


        if (awaitingScanResponse)
        {
            if (lowerInput == "yes")
            {
                if (virusTerminated)
                {
                    response.Add("No more virus detected. Your computer is clean!");
                    await Task.Delay(2500);
                }
                else
                {
                    response.Add("Processing");
                    await Task.Delay(2000);

                    response.Add("Access denied - C:rnd");
                    response.Add("Access denied - C:bootmgr");
                    response.Add("Unable to change attribute - C:asfilesystem");
                    response.Add("There is a virus detected! Do you wish to terminate this? (yes/no)");
                    awaitingTerminateResponse = true; 
                }
                awaitingScanResponse = false; 
            }
            else if (lowerInput == "no")
            {
                response.Add("Scanning aborted.");
                awaitingScanResponse = false; 
            }
            else
            {
                response.Add("Invalid response. Please answer with 'yes' or 'no'.");
            }

            return response;
        }

        if (lowerInput == "help")
        {
            ListEntry("help", "returns a list of commands");
            ListEntry("attrib", "change computer file or directory");
            ListEntry("exit", "exits the system");
            return response;
        }

        if (lowerInput == "attrib")
        {
            awaitingScanResponse = true; 
            response.Add("Do you want to use attrib -r -a -s -h *.*? (yes/no)");
            return response;
        }

        response.Add("The word that you entered is not recognized as an internal or external command, operable program or batch file.");
        response.Add("For more information on a specific command, type 'help' command-name.");
        return response;
    }

    public string ColorString(string s, string color)
    {
        string leftTag = "<color=" + color + ">";
        string rightTag = "</color>";

        return leftTag + s + rightTag;
    }

    void ListEntry(string a, string b)
    {
        response.Add(ColorString(a, colors["orange"]) + ": " + ColorString(b, colors["yellow"]));
    }
}


        