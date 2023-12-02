/* Task of creating a text adventure, where it has a beginning and at least three possible endings
Tasks: Give the user at least two options and accept user input */

#include <iostream>
#include <string>

int main() {
    int satisfied = 0;
    int notsatisfied = 0;
    int answer1;
    int answer2;
    int answer3;
    int max = 0;


    std::cout << "Start of your Adventure\n\n";
    std::cout << "You are a restaurant chef and get to cook three different meals for a celebrity.\n";
    std::cout << "The celebrity is a picky eater and is full of temperament\n";
    std::cout << "For breakfast, you get to choose three meals\n\n";

    std::cout << "You have prepared the ingredients for the following meals and decided to cook for breakfast:\n";
    std::cout << "1) Cereal with oat milk and scrambled eggs as a side.\n";
    std::cout << "2) French Toast with fresh Avocados\n";
    std::cout << "3) Nothing\n";

    std::cin >> answer1;

    // If the celebrity is satisfied/not satisfied, two points otherwise if he's indifferent but not angry, one point each is given to both moods.

    if (answer1 == 1) {
        std::cout << "You have chosen to cook cereal with oat milk and scrambled eggs as a side.\n";
        std::cout << "The celebrity is pleased but not satiated\n\n";
        satisfied++, notsatisfied++;
    } 

    else if  (answer1 == 2) {
        std::cout << "You have chosen to cook french toast with fresh avocados.\n";
        std::cout << "The celebrity is most pleased and appreciates the fresh avocados.\n\n";
        satisfied = 2;
    }

    else if (answer1 == 3) {
        std::cout << "You have chosen to cook nothing, leaving the celebrity starving.\n";
        std::cout << "The celebrity curses at you and you hear his stomach audibly rumbling.\n\n";
        notsatisfied += 2;
    }

    else {
        std::cout << "Invalid answer, please restart your adventure\n\n";

    }

    std::cout << "\n\n";
    std::cout << "It's time for lunch, the celebrity is eagerly waiting.\n";
    std::cout << "You have prepared the ingredients for the following meals:\n\n";

    std::cout << "1) Trout with garlic lemon butter herb sauce\n";
    std::cout << "2) Oven baked fries accompanied with a T-Bone steak\n";
    std::cout << "3) Lentil soup with sour-dough bread\n";

    std::cin >> answer2;

    if (answer2 == 1) {
        std::cout << "The celebrity is most satisfied and acknowledges your cooking skills.\n\n";
        satisfied += 2;
    }

    else if (answer2 == 2) {
        std::cout << "The celebrity is fully satiated but felt the fries and the steak were a tad bit dry.\n\n";
        satisfied++, notsatisfied++;
    }

    else if (answer2 == 3) {
        std::cout << "The celebrity is angered by your choice of meal and is deeply insulted.\n\n";
        notsatisfied += 2;
    }

    else {
        std::cout << "Invalid answer, please restart your adventure.\n";
    }

    std::cout << "\n\n";
    std::cout << "Time for the final showdown, it's time to cook dinner.\n";
    std::cout << "Please choose of the following meals to cook the grand finale for the celebrity:\n\n";

    std::cout << "1) Baked shrimp scampi topped off with lemon zests.\n";
    std::cout << "2) Garlic parmesan encrusted lamb\n";
    std::cout << "3) Turkey pot pie\n";

    std::cin >> answer3;

    if (answer3 == 1) {
        std::cout << "The celebrity is very pleased and asks for another serving\n\n";
        satisfied+2;
    }

    else if (answer3 == 2) {
        std::cout << "The celebrity does not like garlic! He leaves the table after one bite.\n\n";
        notsatisfied += 2;
    }
    
    else if (answer3 == 3) {
        std::cout << "The celebrity appreciates the light meal and thanks you for this simple but yet complex meal.\n\n";
        satisfied++, notsatisfied++;
    }

    else {
        std::cout << "Invalid answer, please restart your adventure.\n\n";
    }

    // Ending now planned, where all points are counted together and the max results will be displayed for the user.

    std::cout << "\n\n";
    std::cout << "Based on the meals you have cooked, the celebrity is:\n\n";
    std::string mood;
    
    if (satisfied > max) {
        max = satisfied;
        mood = "satisfied";
    }

    if (notsatisfied > max) {
        max = notsatisfied;
        mood = "not satisfied";
    }

    // Total amount of points should be displayed and calculating which is higher than the other will display the final mood.
    std::cout << "\n\n";
    std::cout << "Satisfied Points: " << satisfied << "\n";
    std::cout << "Not Satisfied Points: "  << notsatisfied << "\n\n";
    std::cout << "Final Mood: " << mood << "!\n";

}