﻿using Microsoft.Bot.Builder;
using Microsoft.Teams.AI.AI;
using Microsoft.Teams.AI.State;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Teams.AI
{
    /// <summary>
    /// A builder class for simplifying the creation of an Application instance.
    /// </summary>
    /// <typeparam name="TState">Optional. Type of the turn state. This allows for strongly typed access to the turn state.</typeparam>
    public class ApplicationBuilder<TState>
        where TState : TurnState, IMemory, new()
    {
        /// <summary>
        /// The application's configured options.
        /// </summary>
        public ApplicationOptions<TState> Options { get; } = new();

        /// <summary>
        /// Configures the application to use long running messages.
        /// Default state for longRunningMessages is false
        /// </summary>
        /// <param name="adapter">The adapter to use for routing incoming requests.</param>
        /// <param name="botAppId">The Microsoft App ID for the bot.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithLongRunningMessages(BotAdapter adapter, string botAppId)
        {
            if (string.IsNullOrEmpty(botAppId))
            {
                throw new ArgumentException("The ApplicationOptions.LongRunningMessages property is unavailable because botAppId cannot be null or undefined.");
            }

            Options.LongRunningMessages = true;
            Options.Adapter = adapter;
            Options.BotAppId = botAppId;
            return this;
        }

        /// <summary>
        /// Configures the storage system to use for storing the bot's state.
        /// </summary>
        /// <param name="storage">The storage system to use.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithStorage(IStorage storage)
        {
            Options.Storage = storage;
            return this;
        }

        /// <summary>
        /// Configures the AI system to use for processing incoming messages.
        /// </summary>
        /// <param name="aiOptions">The options for the AI system.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithAIOptions(AIOptions<TState> aiOptions)
        {
            Options.AI = aiOptions;
            return this;
        }

        /// <summary>
        /// Configures the turn state factory to use for managing the bot's turn state.
        /// </summary>
        /// <param name="turnStateFactory">The turn state factory to use.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithTurnStateFactory(Func<TState> turnStateFactory)
        {
            Options.TurnStateFactory = turnStateFactory;
            return this;
        }

        /// <summary>
        /// Configures the Logger factory for the application
        /// </summary>
        /// <param name="loggerFactory">The Logger factory</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithLoggerFactory(ILoggerFactory loggerFactory)
        {
            Options.LoggerFactory = loggerFactory;
            return this;
        }

        /// <summary>
        /// Configures the processing of Adaptive Card requests.
        /// </summary>
        /// <param name="adaptiveCardOptions">The options for Adaptive Cards.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithAdaptiveCardOptions(AdaptiveCardsOptions adaptiveCardOptions)
        {
            Options.AdaptiveCards = adaptiveCardOptions;
            return this;
        }

        /// <summary>
        /// Configures the processing of Task Module requests.
        /// </summary>
        /// <param name="taskModulesOptions">The options for Task Modules.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithTaskModuleOptions(TaskModulesOptions taskModulesOptions)
        {
            Options.TaskModules = taskModulesOptions;
            return this;
        }

        /// <summary>
        /// Configures the removing of mentions of the bot's name from incoming messages.
        /// Default state for removeRecipientMention is true
        /// </summary>
        /// <param name="removeRecipientMention">The boolean for removing recipient mentions.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> SetRemoveRecipientMention(bool removeRecipientMention)
        {
            Options.RemoveRecipientMention = removeRecipientMention;
            return this;
        }

        /// <summary>
        /// Configures the typing timer when messages are received.
        /// Default state for startTypingTimer is true
        /// </summary>
        /// <param name="startTypingTimer">The boolean for starting the typing timer.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> SetStartTypingTimer(bool startTypingTimer)
        {
            Options.StartTypingTimer = startTypingTimer;
            return this;
        }

        /// <summary>
        /// Configures authentication for the application.
        /// </summary>
        /// <param name="adapter">The bot adapter.</param>
        /// <param name="authenticationOptions">The options for authentication.</param>
        /// <returns>The ApplicationBuilder instance.</returns>
        public ApplicationBuilder<TState> WithAuthentication(BotAdapter adapter, AuthenticationOptions<TState> authenticationOptions)
        {
            Options.Adapter = adapter;
            Options.Authentication = authenticationOptions;
            return this;
        }

        /// <summary>
        /// Builds and returns a new Application instance.
        /// </summary>
        /// <returns>The Application instance.</returns>
        public Application<TState> Build()
        {
            return new Application<TState>(Options);
        }
    }
}
