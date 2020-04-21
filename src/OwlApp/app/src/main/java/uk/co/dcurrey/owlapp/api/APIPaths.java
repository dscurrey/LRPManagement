package uk.co.dcurrey.owlapp.api;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;

import uk.co.dcurrey.owlapp.R;

public class APIPaths
{
    public static String getCharURL(Context context)
    {
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(context);
        String api_url = sp.getString("pref_api_url", context.getString(R.string.pref_api_url));
        String api_port = sp.getString("pref_char_port", context.getString(R.string.pref_char_port));
        String api_path = sp.getString("pref_api_path", context.getString(R.string.pref_api_path));
        return "https://" + api_url + ":" + api_port + api_path;
    }

    public static String getSkillURL(Context context)
    {
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(context);
        String api_url = sp.getString("pref_api_url", context.getString(R.string.pref_api_url));
        String api_port = sp.getString("pref_skill_port", context.getString(R.string.pref_skill_port));
        String api_path = sp.getString("pref_api_path", context.getString(R.string.pref_api_path));
        return "https://" + api_url + ":" + api_port + api_path;
    }

    public static String getPlayerURL(Context context)
    {
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(context);
        String api_url = sp.getString("pref_api_url", context.getString(R.string.pref_api_url));
        String api_port = sp.getString("pref_player_port", context.getString(R.string.pref_player_port));
        String api_path = sp.getString("pref_api_path", context.getString(R.string.pref_api_path));
        return "https://" + api_url + ":" + api_port + api_path;
    }
}
