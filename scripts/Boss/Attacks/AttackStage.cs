public enum AttackStage
{
	Ready,		// Zeigt dem Boss Skript, dass die Attacke bereit ist
	Indicator,	// Die Anzeige, dass der Boss eine Attacke vorbereitet
	Attack,		// Eigentliche Animation des Angriffs
	Downtime,	// Zeit, in der der Boss nach einem Angriff nichts tut. Kann man mit Attack speed vergleichen
	Finished	// Zeigt dem Boss Skript, dass es die nächste Attacke aussuchen kann
}
