using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class soundEngine 
{   
    public static float sfx_volume = 0.1f;
    public static float bg_volume = 0.5f;
    //private static float gui_volume = 0.5f;
   
    public enum sound_type
    {
            //ui
        click
        ,level_up
        ,quest_accepted
        ,quest_completed
            //player
        ,player_move
        ,player_shoot
        ,player_melee
		,player_burning
        ,player_melee2
			//enemies
		,dragon_move
        ,dragon_shoot
        ,troll_hurt
        ,ghost_hurt
        ,skeleton_hurt
        ,wolf_hurt
        ,troll_attack
            //attacks effects
        ,energy_explosion
        ,fireball_explosion
            //traps
        ,trap_on
        ,trap_off
            //treasures
        ,coin_chest
        ,gem_chest
            //doors and zones
        ,door_open
        ,door_closed
        ,safe_zone
            //boss
        ,meow_question
        ,meow
		,meow2
		,meow3
		,meow4
		,meow5
		,meow6
		,meow7
		,meow8
		,lava
		,meow_angry
		,helmet_clank
		,boss_vanish
		,rock_break
		,smash_pbaoe
		,boss_death
		,boss_death_scream
		,boss_shield
		,boss_death_armor
		,boss_laugh
		,boss_player_death
		,boss_magic_field
		,boss_orb_lightning
		,boss_humming
		,rock_hole_reverse
		,boss_charge_ball_hit
		,boss_charge_hum
		,boss_charge_crackling
		,boss_charge_electric_charge
		,boss_stare
		,boss_gulp
		,boss_fish_flapping
		//gui interactions
		, inventory
        ,shop
        ,buy
        ,sell
        ,potion
        ,pick_item
        ,drop_item
            //level bg music
        ,level1_bg
        ,level2_bg
        ,level3_bg
    }
    public static AudioClip get_clip (sound_type sound)
    {
        switch(sound)
        {
            //ui
            case sound_type.click:
                return soundAssets.instance.click;
            case sound_type.level_up:
                return soundAssets.instance.level_up;
            case sound_type.quest_accepted:
                return soundAssets.instance.quest_accepted;
            case sound_type.quest_completed:
                return soundAssets.instance.quest_completed;
            //player
            case sound_type.player_move:
                return soundAssets.instance.player_move;
            case sound_type.player_shoot:
                return soundAssets.instance.player_shoot;
            case sound_type.player_melee:
                return soundAssets.instance.player_melee;
			case sound_type.player_burning:
				return soundAssets.instance.player_burning;
            case sound_type.player_melee2:
                return soundAssets.instance.player_melee2;
			//enemies
			case sound_type.dragon_move:
                return soundAssets.instance.dragon_move;
            case sound_type.dragon_shoot:
                return soundAssets.instance.dragon_shoot;
            case sound_type.troll_hurt:
                return soundAssets.instance.troll_hurt;
            case sound_type.ghost_hurt:
                return soundAssets.instance.ghost_hurt;
            case sound_type.skeleton_hurt:
                return soundAssets.instance.skeleton_hurt;
            case sound_type.wolf_hurt:
                return soundAssets.instance.wolf_hurt;
            case sound_type.troll_attack:
                return soundAssets.instance.troll_attack;
            //attacks sfx
            case sound_type.energy_explosion:
                return soundAssets.instance.energy_explosion;
            case sound_type.fireball_explosion:
                return soundAssets.instance.fireball_explosion;
                //traps
            case sound_type.trap_on:
                return soundAssets.instance.trap_on;
            case sound_type.trap_off:
                return soundAssets.instance.trap_off;
                //treasures
            case sound_type.coin_chest:
                return soundAssets.instance.coin_chest;
            case sound_type.gem_chest:
                return soundAssets.instance.gem_chest;
                //doors and zones
            case sound_type.door_open:
                return soundAssets.instance.door_open;
            case sound_type.door_closed:
                return soundAssets.instance.door_closed;
            case sound_type.safe_zone:
                return soundAssets.instance.safe_zone;
			//boss
			case sound_type.boss_stare:
				return soundAssets.instance.boss_stare;
			case sound_type.meow_question:
                return soundAssets.instance.meow_question;
            case sound_type.meow:
                return soundAssets.instance.meow;
			case sound_type.meow2:
				return soundAssets.instance.meow2;
			case sound_type.meow3:
				return soundAssets.instance.meow3;
			case sound_type.meow4:
				return soundAssets.instance.meow4;
			case sound_type.meow5:
				return soundAssets.instance.meow5;
			case sound_type.meow6:
				return soundAssets.instance.meow6;
			case sound_type.meow7:
				return soundAssets.instance.meow7;
			case sound_type.meow8:
				return soundAssets.instance.meow8;
			case sound_type.lava:
                return soundAssets.instance.lava;
			case sound_type.meow_angry:
				return soundAssets.instance.meow_angry;
			case sound_type.helmet_clank:
				return soundAssets.instance.helmet_clank;
			case sound_type.boss_vanish:
				return soundAssets.instance.boss_vanish;
			case sound_type.rock_break:
				return soundAssets.instance.rock_break;
			case sound_type.boss_death:
				return soundAssets.instance.boss_death;
			case sound_type.smash_pbaoe:
				return soundAssets.instance.smash_pbaoe;
			case sound_type.boss_death_scream:
				return soundAssets.instance.boss_death_scream;
			case sound_type.boss_death_armor:
				return soundAssets.instance.boss_death_armor;
			case sound_type.boss_shield:
				return soundAssets.instance.boss_shield;
			case sound_type.boss_laugh:
				return soundAssets.instance.boss_laugh;
			case sound_type.boss_player_death:
				return soundAssets.instance.boss_player_death;
			case sound_type.boss_magic_field:
				return soundAssets.instance.boss_magic_field;
			case sound_type.boss_humming:
				return soundAssets.instance.boss_humming;
			case sound_type.boss_orb_lightning:
				return soundAssets.instance.boss_orb_lightning;
			case sound_type.rock_hole_reverse:
				return soundAssets.instance.rock_hole_reverse;
			case sound_type.boss_charge_ball_hit:
				return soundAssets.instance.boss_charge_ball_hit;
			case sound_type.boss_charge_crackling:
				return soundAssets.instance.boss_charge_crackling;
			case sound_type.boss_charge_electric_charge:
				return soundAssets.instance.boss_charge_electric_charge;
			case sound_type.boss_charge_hum:
				return soundAssets.instance.boss_charge_hum;
			case sound_type.boss_gulp:
				return soundAssets.instance.boss_gulp;
			case sound_type.boss_fish_flapping:
				return soundAssets.instance.boss_fish_flapping;
			//gui interactions
			case sound_type.inventory:
                return soundAssets.instance.inventory;
            case sound_type.shop:
                return soundAssets.instance.shop;
            case sound_type.buy:
                return soundAssets.instance.buy;
            case sound_type.sell:
                return soundAssets.instance.sell;
            case sound_type.potion:
                return soundAssets.instance.potion;
            case sound_type.pick_item:
                return soundAssets.instance.pick_item;
            case sound_type.drop_item:
                return soundAssets.instance.drop_item;
                //level bg music
            case sound_type.level1_bg:
                return soundAssets.instance.level1_bg;
            case sound_type.level2_bg:
                return soundAssets.instance.level2_bg;
            case sound_type.level3_bg:
                return soundAssets.instance.level3_bg;
            default: return null;
        }
    }
    public static float get_volume(sound_type s)
    {
        switch (s)
        {
            default: return sfx_volume;
        }
    }
    public static void play_sound(sound_type s)
    {
        GameObject sound = new GameObject("sound");
        AudioSource source =  sound.AddComponent<AudioSource>();
        source.PlayOneShot(get_clip(s), get_volume(s));
        GameObject.Destroy(sound, 2.5f);
    }
	public static void play_sound(sound_type s,float v)
    {
        GameObject sound = new GameObject("sound");
        AudioSource source = sound.AddComponent<AudioSource>();
        source.PlayOneShot(get_clip(s), sfx_volume);
        GameObject.Destroy(sound, 2.5f);
    }
    public static void play_sound_with_volume(sound_type s, float v)
    {
        GameObject sound = new GameObject("sound");
        AudioSource source = sound.AddComponent<AudioSource>();
        source.PlayOneShot(get_clip(s), v);
        GameObject.Destroy(sound, 2.5f);
    }
    public static void play_sound_timed(sound_type s, float time)
	{
		GameObject sound = new GameObject("sound");
		AudioSource source = sound.AddComponent<AudioSource>();
		source.PlayOneShot(get_clip(s), sfx_volume);
		GameObject.Destroy(sound, time);
	}
	public static void play_looped(sound_type s)
    {
        GameObject sound = new GameObject("sound_looped");
        AudioSource source = sound.AddComponent<AudioSource>();
        source.loop = true;
        source.PlayOneShot(get_clip(s), get_volume(s));
    }
    public static void play_looped(sound_type s,float v)
    {
        GameObject sound = new GameObject("sound_looped");
        AudioSource source = sound.AddComponent<AudioSource>();
        source.loop = true;
        source.PlayOneShot(get_clip(s), sfx_volume);
    }
    public static void play_from_side(sound_type s,float side)
    {
        GameObject sound = new GameObject("sound");
        AudioSource source = sound.AddComponent<AudioSource>();
        source.panStereo = side;
        source.PlayOneShot(get_clip(s), get_volume(s));
        GameObject.Destroy(sound, 2.5f);
    }
    public static void play_3d_sound(sound_type s, Vector3 position)
    {
        GameObject sound = new GameObject("sound");
        sound.transform.localPosition = position;
        AudioSource source = sound.AddComponent<AudioSource>();
        source.spatialBlend =1;
        source.PlayOneShot(get_clip(s), get_volume(s));
        GameObject.Destroy(sound, 2.5f);
    }
}
