public class NormalDamageTake : IDamageTakeLogic {

	private int damageResistance = 0;

	public int takeDamage(int damage) {
	
		return damage - damageResistance;
	}

}
