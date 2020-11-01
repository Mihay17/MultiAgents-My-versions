using System;

namespace MultiAgents {
	public class Agent {
		private int agentID;
		private float startPosition;
		private float length;
		private float currentPosition;
		//private int curPos = 0;
		//private int maxPosition;
		private int iterup;

		                                                             // 288
		public Agent(int agentID, float startPosition, float length, int maxPosition, int iterup) {
			this.agentID = agentID;
			this.startPosition = startPosition;
			this.length = length;
			//this.startPosition = startPosition;
			//this.maxPosition = maxPosition;
			this.iterup = iterup;
		}


		public int getIterup()
        {
			return this.iterup;
        }
		public int getAgentID() {
			return this.agentID;
		}

		public float getCurrentPosition()
		{
			return this.currentPosition;
		}
		public float getStartPosition() {
			return this.startPosition;
		}

		public float getLength() {
			return this.length;
		}

		public void setAgentID(int agentID) {
			this.agentID = agentID;
		}

		public void setLength(int length) {
			this.length = length;
		}

		public void setStartPosition(float startPosition) {
			this.startPosition = startPosition;
		}
		public void setCurrentPosition(float currentPosition)
		{
			this.currentPosition = currentPosition;
		}

		/*
		public void runForward(int ID) {
			//currentPosition = startPosition; // 0
			currentPosition = curPos + startPosition; // 0
			int i = 0;
			bool finish = false;
			                                                // 288     
			while ((finish == false) & (currentPosition < maxPosition)) 
			{
				for (i = currentPosition; i < (length + currentPosition); i++) 
				{
					if (Schedule.getValue(i) != Schedule.FREE) 
					{
						finish = false;
						currentPosition = i + 1;

						break;
					}
					else 
					{
						finish = true;
						Schedule.setValue(i, Schedule.OCCUPIED);
						Schedule.arrayOfIdCels[i].SetIdCels(ID, Schedule.OCCUPIED);
					}
				}

			}
			curPos = currentPosition;
			//startPosition = currentPosition;
		}
		*/

		public string toString() {
			string result = "";
			result += "agentID = " + agentID + "\n";
			result += "startPosition = " + startPosition + "\n";
			result += "length = " + length + "\n";
			result += "iterup = " + iterup + "\n";
			return result;
		}







	}
}
