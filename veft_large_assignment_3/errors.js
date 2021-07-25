const { ApolloError, UserInputError } = require('apollo-server');

class PickupGameExceedMaximumError extends ApolloError {
    constructor(message = 'Pickup game has exceeded the maximum of players.') {
        super(message, null, null);
        this.name = 'PickupGameExceedMaximumError';
        this.code = 409;
    }
};


class DurationNotAllowedError extends ApolloError {
    constructor(message = 'The duration is either too short or too long') {
        super(message, null, null);
        this.name = 'DurationNotAllowedError';
        this.code = 400;
    }
};

class TimeHasPassedError extends ApolloError {
    constructor(message = 'Cannot create date in the past.') {
        super(message, null, null);
        this.name = 'TimeHasPassedError';
        this.code = 400;
    }
};
class MixedDatesError extends ApolloError {
    constructor(message = 'Start is more recent than End.') {
        super(message, null, null);
        this.name = 'MixedDatesError';
        this.code = 400;
    }
};

class BasketballFieldClosedError extends ApolloError {
    constructor(message = 'Cannot add a pickup game to a closed basketball field') {
        super(message, null, null);
        this.name = 'BasketballFieldClosedError';
        this.code = 400;
    }
};

class PickupGameOverlapError extends ApolloError {
    constructor(message = 'Pickup games cannot overlap') {
        super(message, null, null);
        this.name = 'PickupGameOverlapError';
        this.code = 400;
    }
};

class PickupGameAlreadyPassedError extends ApolloError {
    constructor(message = 'Pickup game has already passed') {
        super(message, null, null);
        this.name = 'PickupGameAlreadyPassedError';
        this.code = 400;
    }
}

class NotFoundError extends ApolloError {
    constructor(message = 'Id was not found') {
        super(message, null, null);
        this.name = 'NotFoundError';
        this.code = 404;
    }
}

class NotValidIdError extends ApolloError {
    constructor(message = 'Parameter is not a valid MongoDB Id') {
        super(message, null, null);
        this.name = 'NotValidIdError';
        this.code = 404;
    }
}

class PlayerAlreadyRegisteredError extends ApolloError {
    constructor(message = 'Player is already registered') {
        super(message, null, null);
        this.name = 'PlayerAlreadyRegistered';
        this.code = 400;
    }
}

class PlayerInPickupGameOverlapError extends ApolloError {
    constructor(message = 'Pickup games with this player cannot overlap') {
        super(message, null, null);
        this.name = 'PlayerInPickupGameOverlapError';
        this.code = 400;
    }
};

module.exports = {
    PickupGameExceedMaximumError,
    BasketballFieldClosedError,
    PickupGameOverlapError,
    PickupGameAlreadyPassedError,
    NotFoundError,
    UserInputError,
    TimeHasPassedError,
    MixedDatesError,
    DurationNotAllowedError,
    PlayerAlreadyRegisteredError,
    PlayerInPickupGameOverlapError,
    NotValidIdError
};
