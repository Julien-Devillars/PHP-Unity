using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchIdentityFromWeb : FetchDataFromWeb
{
    public List<GameObject> mIdentities;
    public void Start()
    {
        mIdentities = new List<GameObject>();
        foreach (Transform child in transform) 
        {
            mIdentities.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/get_identity_table.php?lang={Translation.lang}&mission={IPConfig.MISSION}";
        StartCoroutine(getData(url, response =>
        {
            if (!string.IsNullOrEmpty(response))
            {
                string[] identities = response.Split("\n");
                string pattern = "\"(.*?)\":\\s+\"(.*?)\"";

                for (int i = 0; i< identities.Length; i++)
                {
                    string identity = identities[i];
                    if (identity.Length == 0) continue;

                    Match match = Regex.Match(identity, pattern);
                    if(match.Groups.Count < 2) continue;
                    mIdentities[i].SetActive(true);

                    TextMeshProUGUI title_text = mIdentities[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI value_text = mIdentities[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                    string key = match.Groups[1].Value;
                    string value = match.Groups[2].Value;

                    title_text.text = key;
                    value_text.text = value;
                }
            }
        }));
    }
}