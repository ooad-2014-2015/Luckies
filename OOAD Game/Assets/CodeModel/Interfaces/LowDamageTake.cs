public class LowDamageTake : IDamageTakeLogic {
	
	private int damageResistance = 20;
	
	public int takeDamage(int damage) {
		
		return damage - damageResistance;
	}
	
}
