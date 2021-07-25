# def update_position():
#     position = current_position()
#     action  = player_action()
    
    

#     if action == ("n" or action == "N"):
        
#         position = position + 0.1
#         print (position)
#         return position
#     elif action == ("s" or action == "S"):
        
#         position = position -0.1
#         print (position)

#     elif action == ("e" or action == "E"):
        
#         position = position + 1
#         print (position)

#     elif action == ("w" or action == "W"):
        
#         position = position -1
#         print (position)

#     else:


#         print("Invalid Input")


# def player_action():

#     action = input("You can travel:")
#     return action
    
# def current_position():
#     start_pos = 1.1


#     win_condition = False

#     while win_condition == False:
        
#         updated_position = update_position()
#         return update_position

# def initialize_game():

#     current_position()

# initialize_game()

# vinctory_condition = False
# def starting_position():
#     starting_position = 1.1
#     return starting_position


# def player_action():

#      action = input("You can travel:")
#      return action

# def update_pos(x):

#     action = player_action()

#     if action == "n":
#         updated_pos = x + 0.1
#         return update_pos
    
#     print(update_pos)


# position = starting_position()

# while vinctory_condition != True:

    
    
#     updated_position = update_pos(player_action,position)

#     update_pos(player_action(), updated_position)

def player_action():
    action = input("Direction: ").lower()    
    return action

def update_position(action):
    current_action = action
    change = 0.0
    if current_action == ("n" or action == "N"):
        
        change = change + 0.1
        
    
    elif current_action == ("s" or action == "S"):
        
        change = change -0.1
        

    elif current_action == ("e" or action == "E"):
        
        change = change + 1
        

    elif current_action == ("w" or action == "W"):
        
        change = change -1

    else:
        print("Invalid Input")

    change = round(change, 1)
    return change

def is_legal(position, action): #takes in the current position
    print("this is our current", position)
    x = position 
    if x == 1.1 and (action == 'start' or action == 'n'):
        print("You can travel: (N)orth.")
        return True
    elif x == 1.2 and (action != 'w'):
        print("You can travel: (N)orth or (E)ast or (S)outh.")

        return True
    elif x == 1.3 and (action == 'e' or action == 's'):
        print("You can travel: (E)ast or (S)outh.")
        return True
    elif x == 2.1 and (action == 'n'):
        print("You can travel: (N)orth.")
        return True
    elif x == 2.2 and (action == 's' or action == 'w'):
        print("You can travel: (S)outh or (W)est.")
        return True
    elif x == 2.3 and (action == 'e' or action == 'w'):
        print("You can travel: (E)ast or (W)est.")
        return True
    # elif x == 3.1 and (action == 'n'):
    #     print("Victory!")
    #     return True
    elif x == 3.2 and (action == 'n' or action == 's'):
        print("You can travel: (N)orth or (S)outh.")
        return True
    elif x == 3.3 and (action == 's' or action == 'w'):
        print("You can travel: (S)outh or (E)ast.")
        return True
    else:
        return False


def start_game():
    position = 1.1
    action = 'start'
    victory_condition = False
    is_legal(position, action)
    action = player_action()
    position = position + update_position(action)
    while victory_condition != True:
        legal = is_legal(position, action)
        action = player_action()
        
        if legal:
            position = position + update_position(action)
            position = round(position, 1)
        else:
            print("Not a valid direction!")
            continue
        if position == 3.1:
            print("Victory!")
            victory_condition = True
start_game()