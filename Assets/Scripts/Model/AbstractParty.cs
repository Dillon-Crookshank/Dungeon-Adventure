using System;

namespace DefaultNamespace; 


internal abstract class AbstractParty {

    AbstractParty() {
        
    }

     private enum partyOrder {
         1,
         2,
         3,
         4,
         5,
         6
     }

    private List<AbstractActor> partyComposition;
    
    private int[2, 3] partyGrid;


}