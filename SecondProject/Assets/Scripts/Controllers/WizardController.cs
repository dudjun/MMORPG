using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : PlayerController
{
	[SerializeField] Item IceWeapon;
	[SerializeField] Item FireWeapon;
	[SerializeField] ParticleSystem IceParticle;
	PlayerStat stat;

	public override void Init()
	{
		base.Init();
		QuickSlot.quickSlots[0].GetComponent<Slot>().AddItem(IceWeapon);
		QuickSlot.quickSlots[1].GetComponent<Slot>().AddItem(FireWeapon);
		stat = GetComponent<PlayerStat>();
	}

	void PlayParticle()
    {
		if (QuickSlot.go_HandWeapon.GetComponent<ItemPickUp>().item == IceWeapon)
        {
			if (stat.Mp >= 10)
			{
				IceParticle.Play();
				stat.Mp -= 10;
			}
			
			if (stat.Mp < 0)
				stat.Mp = 0;
		}
		else if (QuickSlot.go_HandWeapon.GetComponent<ItemPickUp>().item == FireWeapon)
        {
			if (stat.Mp >= 5)
            {
				Managers.Resource.Instantiate("Fire_Particle");
				stat.Mp -= 5;
			}
			
			if (stat.Mp < 0)
				stat.Mp = 0;
		}
    }

	void ActiveIceCollider()
    {
		IceParticle.transform.GetComponent<CapsuleCollider>().enabled = true;
    }

	void InActiveIceCollider()
    {
		IceParticle.transform.GetComponent<CapsuleCollider>().enabled = false;
	}
}
